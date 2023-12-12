using Application.common.DTO;
using Application.common.Interfaces;
using Application.Common.Interfaces;
using Application.common.Mappings;
using Application.common.Models;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.Queries.GetActivity;

public record GetPaginatedListActivitiesQuery : IRequest<PaginatedList<ActivityDTO>>
{
  public PaginatedListParams PaginatedListParams { get; init; }
}

public class
    GetPaginatedListActivitiesQueryHandler :
    IRequestHandler<GetPaginatedListActivitiesQuery,
    PaginatedList<ActivityDTO>>
{
  private readonly IEventsDbContext                                _context;
  private readonly ILogger<GetPaginatedListActivitiesQueryHandler> _logger;
  private readonly IMapper                                         _mapper;
  private readonly IUserService                                    _userService;

  public GetPaginatedListActivitiesQueryHandler(
      IMapper                                         mapper,
      ILogger<GetPaginatedListActivitiesQueryHandler> logger,
      IEventsDbContext                                context,
      IUserService                                    userService)
  {
    _mapper = mapper;
    _logger = logger;
    _context = context;
    _userService = userService;
  }

  public async Task<PaginatedList<ActivityDTO>> Handle(
      GetPaginatedListActivitiesQuery request,
      CancellationToken               cancellationToken)
  {
    var query = _context.Activities.Include(activity => activity.Attendees).AsQueryable();

    // 添加基于新属性的过滤条件
    if (!string.IsNullOrWhiteSpace(request.PaginatedListParams.Title))
    {
      query = query.Where(activity =>
                              activity.Title.Contains(request.PaginatedListParams.Title));
    }

    if (!string.IsNullOrWhiteSpace(request.PaginatedListParams.Category))
    {
      query = query.Where(activity =>
                              activity.Category.Contains(request.PaginatedListParams
                                                             .Category));
    }

    if (!string.IsNullOrWhiteSpace(request.PaginatedListParams.City))
    {
      query = query.Where(activity =>
                              activity.City.Contains(request.PaginatedListParams.City));
    }

    if (!string.IsNullOrWhiteSpace(request.PaginatedListParams.Venue))
    {
      query = query.Where(activity =>
                              activity.Venue.Contains(request.PaginatedListParams.Venue));
    }

    if (request.PaginatedListParams.StartDate.HasValue)
    {
      query = query.Where(activity =>
                              activity.Date
                           >= request.PaginatedListParams.StartDate.Value);
    }

    if (request.PaginatedListParams.EndDate.HasValue)
    {
      query = query.Where(activity =>
                              activity.Date <= request.PaginatedListParams.EndDate.Value);
    }

    // if (!string.IsNullOrWhiteSpace(request.PaginatedListParams.SearchTerm))
    // {
    //   var searchTerm = request.PaginatedListParams.SearchTerm.ToLower().Trim();
    //
    //   query = query.Where(activity =>
    //                           activity.Title.ToLower().Contains(searchTerm)
    //                        || activity.Category.ToLower().Contains(searchTerm)
    //                        || activity.Description.ToLower().Contains(searchTerm)
    //                        || activity.City.ToLower().Contains(searchTerm)
    //                        || activity.Venue.ToLower().Contains(searchTerm));
    // }

    // empty no found
    if (!query.Any())
    {
      _logger.LogError("No activities found with the provided search criteria");

      throw new NotFoundException("No activities found.");
    }

    PaginatedList<Activity>? activities =
        await query.PaginatedListAsync(request.PaginatedListParams.PageNumber,
                                       request.PaginatedListParams.PageSize);

    if (activities == null
     || !activities.Any())
    {
      _logger.LogError("Could not find activities");

      throw new NotFoundException(nameof(Activities));
    }

    // get all user id from activity attendees list
    List<Guid> userIds = activities
                         .SelectMany(activity => activity.Attendees.Select(a => a.UserId))
                         .Distinct()
                         .ToList();

    // get all user info from user service by user id list
    var usersInfo = await _userService.GetUsersInfoByIdsAsync(userIds);

    PaginatedList<ActivityDTO> activitiesDto =
        _mapper.Map<PaginatedList<ActivityDTO>>(activities);

    for (var i = 0; i < activities.Count; i++)
    {
      activitiesDto[i].Attendees = activities[i]
                                   .Attendees.Select(attendee =>
                                   {
                                     // find user info by id for usersInfo list
                                     var userInfoDto =
                                         usersInfo.FirstOrDefault(u =>
                                             u.Id
                                          == attendee.UserId);

                                     // map it to ActivityAttendeeDTO
                                     var activityAttendeeDto = _mapper
                                         .Map<ActivityAttendeeDTO>(userInfoDto);

                                     // set isHost property according to attendee.IsHost property
                                     activityAttendeeDto.IsHost = attendee.IsHost;

                                     return activityAttendeeDto;
                                   })
                                   .ToList();

      activitiesDto[i].HostUsername = activitiesDto[i]
                                      .Attendees
                                      .FirstOrDefault(a => a.IsHost)
                                      ?.UserName
                                   ?? string.Empty;
    }

    return activitiesDto;
  }
}
