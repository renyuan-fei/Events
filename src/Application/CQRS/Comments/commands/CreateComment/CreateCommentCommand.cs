using Application.common.DTO;
using Application.Common.Helpers;
using Application.common.Interfaces;
using Application.Common.Interfaces;

using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using Domain.ValueObjects.Activity;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Comments.commands.CreateComment;

public record CreateCommentCommand : IRequest<CommentDTO>
{
  public string Body       { get; init; } = null!;
  public string ActivityId { get; init; }

  public string UserId { get; init; }
}

public class
    CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CommentDTO>
{
  private readonly IUnitOfWork                          _unitOfWork;
  private readonly IActivityRepository                  _activityRepository;
  private readonly ILogger<CreateCommentCommandHandler> _logger;

  public CreateCommentCommandHandler(
      ILogger<CreateCommentCommandHandler> logger,
      IActivityRepository                  activityRepository,
      IUnitOfWork                          unitOfWork)
  {
    _logger = logger;
    _activityRepository = activityRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task<CommentDTO> Handle(
      CreateCommentCommand request,
      CancellationToken    cancellationToken)
  {
    try
    {
      var body = request.Body;
      var activityId = new ActivityId(request.ActivityId);
      var userId = new UserId(request.UserId);

      var newComment = Comment.Create(userId, body, activityId);

      var activity = await _activityRepository.GetByIdAsync(activityId, cancellationToken);

      GuardValidation.AgainstNull(activity, $"Activity with Id {request.ActivityId} not found");

      activity!.AddComment(newComment);

      var result = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

      if (!result)
      {
        throw new DbUpdateException("Could not create comment");
      }

      throw new DbUpdateException("There was an error saving data to the database");
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
