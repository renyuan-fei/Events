using Application.common.DTO;
using Application.Common.Helpers;
using Application.common.Interfaces;
using Application.Common.Interfaces;
using Application.common.Models;

using AutoMapper.Internal;

using Domain.Repositories;
using Domain.ValueObjects.Activity;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Attendees.Queries.GetActivityAttendeesWithPagination;

public record
    GetPaginatedListAttendeesQuery : IRequest<
    PaginatedList<AttendeeDTO>>
{
  public string ActivityId { get; init; }

  public PaginatedListParams PaginatedListParams { get; init; }
}

public class GetActivityAttendeesWithPaginationQueryHandler : IRequestHandler<
    GetPaginatedListAttendeesQuery, PaginatedList<AttendeeDTO>>
{
  private readonly IActivityRepository _activityRepository;
  private readonly ILogger<GetActivityAttendeesWithPaginationQueryHandler> _logger;
  private readonly IMapper _mapper;
  private readonly IPhotoRepository                     _photoRepository;
  private readonly IUserService                         _userService;

  public GetActivityAttendeesWithPaginationQueryHandler(
      IMapper                                                 mapper,
      ILogger<GetActivityAttendeesWithPaginationQueryHandler> logger,
      IActivityRepository                                     activityRepository,
      IPhotoRepository                                        photoRepository,
      IUserService                                            userService)
  {
    _mapper = mapper;
    _logger = logger;
    _activityRepository = activityRepository;
    _photoRepository = photoRepository;
    _userService = userService;
  }

  public async Task<PaginatedList<AttendeeDTO>> Handle(
      GetPaginatedListAttendeesQuery request,
      CancellationToken              cancellationToken)
  {
    try
    {
      var activityId = new ActivityId(request.ActivityId);
      var pageNumber = request.PaginatedListParams.PageNumber;
      var pageSize = request.PaginatedListParams.PageSize;

      var activity = await _activityRepository.GetActivityWithAttendeesByIdAsync(activityId, cancellationToken);

      GuardValidation.AgainstNull(activity, $"Activity with Id {request.ActivityId} not found");

      if (!activity!.Attendees.Any())
      {
        _logger.LogInformation("No attendees found for activity with Id {ActivityId}", request.ActivityId);
        return new PaginatedList<AttendeeDTO>();
      }

      var userIds = activity!.Attendees.Select(attendee => attendee.Identity.UserId.Value).ToList();;

      var usersTask = _userService.GetUsersByIdsAsync(userIds);
      var photosTask = _photoRepository.GetMainPhotosByOwnerIdAsync(userIds, cancellationToken);

      await Task.WhenAll(usersTask, photosTask);

      GuardValidation.AgainstNullOrEmpty(usersTask.Result, "User information for attendees not found");

      var usersDictionary = usersTask.Result.ToDictionary(u => u.Id);
      var photosDictionary = photosTask.Result.ToDictionary(p => p.OwnerId);

      var result = _mapper.Map<PaginatedList<AttendeeDTO>>(activity!.Attendees);

      result.Items.ForAll(attendee =>
      {
        UserHelper.FillWithPhotoAndUserDetail(attendee, usersDictionary, photosDictionary);
      });

      return result;

    }
    catch (Exception ex)
    {
      _logger.LogError(ex,
                       "Error occurred in {Name}: {ExMessage}",
                       nameof(GetPaginatedListAttendeesQuery),
                       ex
                           .Message);

      throw;
    }
  }
}
