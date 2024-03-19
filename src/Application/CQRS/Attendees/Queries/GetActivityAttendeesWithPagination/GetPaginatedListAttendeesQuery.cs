using Application.common.DTO;
using Application.Common.Helpers;
using Application.common.Interfaces;
using Application.Common.Interfaces;
using Application.common.Mappings;
using Application.common.Models;

using AutoMapper.Internal;

using Domain.Repositories;
using Domain.ValueObjects.Activity;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Attendees.Queries.GetActivityAttendeesWithPagination;

public record
    GetPaginatedListAttendeesQuery : IRequest<
    PaginatedList<AttendeeDto>>
{
  public string ActivityId { get; init; }

  public PaginatedListParams PaginatedListParams { get; init; }
}

public class GetActivityAttendeesWithPaginationQueryHandler : IRequestHandler<
    GetPaginatedListAttendeesQuery, PaginatedList<AttendeeDto>>
{
  private readonly IActivityRepository _activityRepository;
  private readonly ILogger<GetActivityAttendeesWithPaginationQueryHandler> _logger;
  private readonly IMapper _mapper;
  private readonly IPhotoRepository _photoRepository;
  private readonly IUserService _userService;

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

  public async Task<PaginatedList<AttendeeDto>> Handle(
      GetPaginatedListAttendeesQuery request,
      CancellationToken              cancellationToken)
  {
    try
    {
      var activityId = new ActivityId(request.ActivityId);
      var pageNumber = request.PaginatedListParams.PageNumber;
      var pageSize = request.PaginatedListParams.PageSize;
      var initialTimestamp = request.PaginatedListParams.InitialTimestamp;

      var attendees = _activityRepository.GetAttendeeByActivityIdQueryable(activityId);

      var attendeesBeforeTimestamp = attendees.Where(attendee => attendee.Created < initialTimestamp);

      if (!attendeesBeforeTimestamp.Any()) { return new PaginatedList<AttendeeDto>(); }

      var userIds = attendeesBeforeTimestamp.Select(attendee => attendee.Identity.UserId.Value).ToList();
      ;

      var usersTask = _userService.GetUsersByIdsAsync(userIds, cancellationToken);

      var photosTask =
          _photoRepository.GetMainPhotosByOwnerIdAsync(userIds, cancellationToken);

      await Task.WhenAll(usersTask, photosTask);

      GuardValidation.AgainstNullOrEmpty(usersTask.Result,
                                         "User information for attendees not found");

      var usersDictionary = usersTask.Result.ToDictionary(u => u.Id);
      var photosDictionary = photosTask.Result.ToDictionary(p => p.OwnerId);

      var paginatedList = await attendeesBeforeTimestamp
                                .ProjectTo<AttendeeDto>(_mapper.ConfigurationProvider)
                                .PaginatedListAsync(pageNumber, pageSize);

      paginatedList.Items.ForAll(attendee =>
      {
        UserHelper.FillWithPhotoAndUserDetail(attendee,
                                              usersDictionary,
                                              photosDictionary);
      });

      return paginatedList;
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
