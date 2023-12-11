using Application.common.DTO;
using Application.common.Interfaces;
using Application.Common.Interfaces;
using Application.common.Mappings;

using AutoMapper;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.Queries.GetActivity;

public record GetAllActivitiesQuery : IRequest<List<ActivityDTO>>;

public class
    GetAllActivitiesQueryHandler : IRequestHandler<GetAllActivitiesQuery,
    List<ActivityDTO>>
{
  private readonly IEventsDbContext                      _context;
  private readonly ILogger<GetAllActivitiesQueryHandler> _logger;
  private readonly IMapper                               _mapper;
  private readonly IUserService                          _userService;

  public GetAllActivitiesQueryHandler(
      IMapper                               mapper,
      ILogger<GetAllActivitiesQueryHandler> logger,
      IMediator                             mediator,
      IUserService                          userService,
      IEventsDbContext                      context)
  {
    _mapper = mapper;
    _logger = logger;
    _userService = userService;
    _context = context;
  }

  public async Task<List<ActivityDTO>> Handle(
      GetAllActivitiesQuery request,
      CancellationToken     cancellationToken)
  {
    var activities = await
        _context.Activities.Include(a => a.Attendees).ToListAsync(cancellationToken);

    if (!activities.Any())
    {
      _logger.LogError("Could not find activities");

      throw new NotFoundException(nameof(Activity));
    }

    // get all user id from activity attendees list
    var userIds = activities
                  .SelectMany(activity => activity.Attendees.Select(a => a.UserId))
                  .Distinct()
                  .ToList();

    // get all user info from user service by user id list
    var usersInfo = await _userService.GetUsersInfoByIdsAsync(userIds);

    var activitiesDto = _mapper.Map<List<ActivityDTO>>(activities);

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
      activitiesDto[i].HostUsername = activitiesDto[i].Attendees
                                            .FirstOrDefault(a => a.IsHost)
                                            ?.UserName
                              ?? string.Empty;
    }

    return activitiesDto;
  }
}
