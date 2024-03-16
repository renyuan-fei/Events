using Application.common.DTO;
using Application.Common.Helpers;
using Application.common.Interfaces;
using Application.Common.Interfaces;
using Application.common.Mappings;
using Application.common.Models;
using Application.CQRS.Comments.Queries.GetComments;

using AutoMapper.Internal;

using Domain.Repositories;
using Domain.ValueObjects.Activity;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Comments.Queries.GetPaginatedComments;

public record GetPaginatedCommentsQuery : IRequest<PaginatedList<CommentDto>>
{
  public PaginatedListParams PaginatedListParams { get; init; }
  public string              ActivityId          { get; init; }
}

public class GetPaginatedCommentsQueryHandler : IRequestHandler<GetPaginatedCommentsQuery,
    PaginatedList<CommentDto>>
{
  private readonly IPhotoRepository                   _photoRepository;
  private readonly ICommentRepository                 _commentRepository;
  private readonly ILogger<GetPaginatedCommentsQueryHandler> _logger;
  private readonly IUserService                       _userService;
  private readonly IMapper                            _mapper;

  public GetPaginatedCommentsQueryHandler(
      IMapper                            mapper,
      IUserService                       userService,
      ICommentRepository                 commentRepository,
      IPhotoRepository                   photoRepository,
      ILogger<GetPaginatedCommentsQueryHandler> logger)
  {
    _mapper = mapper;
    _userService = userService;
    _commentRepository = commentRepository;
    _photoRepository = photoRepository;
    _logger = logger;
  }

  public async Task<PaginatedList<CommentDto>> Handle(
      GetPaginatedCommentsQuery request,
      CancellationToken         cancellationToken)
  {
    try
    {
      var activityId = new ActivityId(request.ActivityId);
      var pageNumber = request.PaginatedListParams.PageNumber;
      var pageSize = request.PaginatedListParams.PageSize;
      var initialTimestamp = request.PaginatedListParams.InitialTimestamp;

      var comments =
          _commentRepository.GetCommentsByActivityId(activityId, initialTimestamp);

      if (!comments.Any()) { return new PaginatedList<CommentDto>(); }

      var orderByDescending = comments!.OrderByDescending(comment => comment.Created);

      var userIds = orderByDescending.Select(c => c.UserId.Value).ToList();

      var usersTask = _userService.GetUsersByIdsAsync(userIds, cancellationToken);

      var photosTask = _photoRepository.GetMainPhotosByOwnerIdAsync(userIds, cancellationToken);

      await Task.WhenAll(usersTask, photosTask);

      GuardValidation.AgainstNullOrEmpty(usersTask.Result,
                                         "User information for attendees not found");

      var usersDictionary = usersTask.Result.ToDictionary(u => u.Id);
      var photosDictionary = photosTask.Result.ToDictionary(p => p.OwnerId);

      var paginatedComments = await orderByDescending
                              .ProjectTo<CommentDto>(_mapper.ConfigurationProvider)
                              .PaginatedListAsync(pageNumber, pageSize);

      paginatedComments.Items.ForAll(comment => UserHelper.FillWithPhotoAndUserDetail(comment,
          usersDictionary, photosDictionary));

      return paginatedComments;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error saving to the database: {ExMessage}", ex.Message);

      throw;
    }
  }
}
