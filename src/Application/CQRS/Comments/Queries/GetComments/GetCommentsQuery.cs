using Application.common.DTO;
using Application.Common.Helpers;
using Application.common.Interfaces;
using Application.Common.Interfaces;

using AutoMapper.Internal;

using Domain.Repositories;
using Domain.ValueObjects.Activity;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Comments.Queries.GetComments;

public record GetCommentsQuery : IRequest<List<CommentDto>>
{
  public string ActivityId { get; init; }
}

public class GetCommentsQueryHandler : IRequestHandler<GetCommentsQuery, List<CommentDto>>
{
  private readonly IPhotoRepository                 _photoRepository;
  private readonly ICommentRepository               _commentRepository;
  private readonly ILogger<GetCommentsQueryHandler> _logger;
  private readonly IMapper                          _mapper;
  private readonly IUserService                     _userService;

  public GetCommentsQueryHandler(
      IMapper                          mapper,
      ILogger<GetCommentsQueryHandler> logger,
      ICommentRepository               commentRepository,
      IUserService                     userService,
      IPhotoRepository                 photoRepository)
  {
    _mapper = mapper;
    _logger = logger;
    _commentRepository = commentRepository;
    _userService = userService;
    _photoRepository = photoRepository;
  }

  public async Task<List<CommentDto>> Handle(
      GetCommentsQuery  request,
      CancellationToken cancellationToken)
  {
    try
    {
      var activityId = new ActivityId(request.ActivityId);
      var comments = _commentRepository.GetCommentsByActivityId(activityId);

      if (!comments.Any())
      {
        return new List<CommentDto>();
      }

      var userIds = comments.Select(c => c.UserId.Value).ToList();

      var usersTask = _userService.GetUsersByIdsAsync(userIds,cancellationToken);

      var photosTask =
          _photoRepository.GetMainPhotosByOwnerIdAsync(userIds, cancellationToken);

      await Task.WhenAll(usersTask, photosTask);

      GuardValidation.AgainstNullOrEmpty(usersTask.Result,
                                         "User information for attendees not found");

      var usersDictionary = usersTask.Result.ToDictionary(u => u.Id);
      var photosDictionary = photosTask.Result.ToDictionary(p => p.OwnerId);

      var result = _mapper.Map<List<CommentDto>>(comments);

      result.ForAll(comment => UserHelper.FillWithPhotoAndUserDetail(comment, usersDictionary, photosDictionary));

      return result;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex,
                       "Error occurred in {Name}: {ExMessage}",
                       nameof(GetCommentsQuery),
                       ex
                           .Message);

      throw;
    }
  }
}
