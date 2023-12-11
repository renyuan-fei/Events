using Application.common.DTO;
using Application.common.Interfaces;
using Application.Common.Interfaces;

using AutoMapper;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.Queries.GetActivity;

public record GetActivityByIdQuery : IRequest<ActivityDTO>
{
  public Guid Id { get; init; }
}

public class
    GetActivityByIdQueryHandler : IRequestHandler<GetActivityByIdQuery, ActivityDTO>
{
  private readonly IEventsDbContext                     _context;
  private readonly ILogger<GetActivityByIdQueryHandler> _logger;
  private readonly IMapper                              _mapper;
  private readonly IUserService                         _userService;

  public GetActivityByIdQueryHandler(
      IMapper                              mapper,
      ILogger<GetActivityByIdQueryHandler> logger,
      IEventsDbContext                     context,
      IUserService                         userService)
  {
    _mapper = mapper;
    _logger = logger;
    _context = context;
    _userService = userService;
  }

  public async Task<ActivityDTO> Handle(
      GetActivityByIdQuery request,
      CancellationToken    cancellationToken)
  {
    var activity = await _context.Activities.Include(a => a.Attendees)
                                 .SingleOrDefaultAsync(a => a.Id == request.Id,
                                                       cancellationToken);

    if (activity == null)
    {
      _logger.LogError("Could not find activity with id {Id}", request.Id);

      throw new NotFoundException(nameof(Activity), request.Id);
    }

    try
    {
      // get all user id from activity attendees list
      var userIds = activity.Attendees.Select(a => a.UserId).Distinct().ToList();

      // get all user info from user service by user id list
      var usersInfo = await _userService.GetUsersInfoByIdsAsync(userIds);

      // map user info to activity attendees
      var activityDto = _mapper.Map<ActivityDTO>(activity);

      // map userInfo to activity attendee
      activityDto.Attendees = activity.Attendees.Select(attendee =>
                                      {
                                        // find user info by id for usersInfo list
                                        var userInfoDto =
                                            usersInfo.FirstOrDefault(u =>
                                                     u.Id == attendee.UserId);

                                        // map it to ActivityAttendeeDTO
                                        var activityAttendeeDto =  _mapper
                                            .Map<ActivityAttendeeDTO>(userInfoDto);

                                        // set isHost property according to attendee.IsHost property
                                        activityAttendeeDto.IsHost = attendee.IsHost;

                                        return activityAttendeeDto;
                                      })
                                      .ToList();

      // set host username
      activityDto.HostUsername = activityDto.Attendees
                                            .FirstOrDefault(a => a.IsHost)
                                            ?.UserName
                              ?? string.Empty;

      return activityDto;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error Mapping to DTO: {ExMessage}", ex.Message);

      throw;
    }
  }
}
