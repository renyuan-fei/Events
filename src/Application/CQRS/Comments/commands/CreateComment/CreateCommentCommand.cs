using Application.common.DTO;
using Application.Common.Helpers;
using Application.common.Interfaces;
using Application.Common.Interfaces;

using Domain.Constant;
using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using Domain.ValueObjects.Activity;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Comments.commands.CreateComment;

public record CreateCommentCommand : IRequest<CommentDto>
{
  public string Body       { get; init; } = null!;
  public string ActivityId { get; init; }

  public string UserId { get; init; }
}

public class
    CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CommentDto>
{
  private readonly IPhotoRepository                     _photoRepository;
  private readonly IUserService                         _userService;
  private readonly IMapper                              _mapper;
  private readonly IUnitOfWork                          _unitOfWork;
  private readonly IActivityRepository                  _activityRepository;
  private readonly ILogger<CreateCommentCommandHandler> _logger;

  public CreateCommentCommandHandler(
      ILogger<CreateCommentCommandHandler> logger,
      IActivityRepository                  activityRepository,
      IUnitOfWork                          unitOfWork,
      IMapper                              mapper,
      IUserService                         userService,
      IPhotoRepository                     photoRepository)
  {
    _logger = logger;
    _activityRepository = activityRepository;
    _unitOfWork = unitOfWork;
    _mapper = mapper;
    _userService = userService;
    _photoRepository = photoRepository;
  }

  public async Task<CommentDto> Handle(
      CreateCommentCommand request,
      CancellationToken    cancellationToken)
  {
    try
    {
      var body = request.Body;
      var activityId = new ActivityId(request.ActivityId);
      var userId = new UserId(request.UserId);

      var user = await _userService.GetUserByIdAsync(userId.Value, cancellationToken);

      GuardValidation.AgainstNull(user, $"User with Id {request.UserId} not found");

      var newComment = Comment.Create(userId, body, activityId);

      var activity = await _activityRepository.GetByIdAsync(activityId, cancellationToken);

      GuardValidation.AgainstNull(activity, $"Activity with Id {request.ActivityId} not found");

      activity!.AddComment(newComment);

      var result = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

      if (!result)
      {
        throw new DbUpdateException("Could not create comment");
      }

      var photo = await _photoRepository.GetMainPhotoByOwnerIdAsync(userId.Value,
          cancellationToken) ;

      var value = _mapper.Map<CommentDto>(newComment);

      value.UserName = user.UserName;
      value.DisplayName = user.DisplayName;

      value.Image = photo == null
          ? DefaultImage.DefaultUserImageUrl
          : photo.Details.Url;

      value.Bio = user.Bio;

      return value;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex,
                       "Error occurred in {Name}: {ExMessage}",
                       nameof(CreateCommentCommand),
                       ex
                           .Message);

      throw;
    }
  }
}
