using Application.common.Interfaces;
using Application.Common.Interfaces;

using AutoMapper;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.ActivityAttendee.Commands.UpdateActivityAttendee;

public record UpdateActivityAttendeeCommand : IRequest<Unit>
{
  public Guid Id         { get; init; }
  public Guid ActivityId { get; init; }
}

public class
    UpdateActivityAttendeeHandler : IRequestHandler<UpdateActivityAttendeeCommand, Unit>
{
  private readonly IEventsDbContext                  _context;
  private readonly ILogger<UpdateActivityAttendeeHandler> _logger;
  private readonly IMapper                                _mapper;
  private readonly IUserService                           _userService;

  public UpdateActivityAttendeeHandler(
      IEventsDbContext                  context,
      IMapper                                mapper,
      ILogger<UpdateActivityAttendeeHandler> logger,
      IUserService                           userService)
  {
    _context = context;
    _mapper = mapper;
    _logger = logger;
    _userService = userService;
  }

  public async Task<Unit> Handle(
      UpdateActivityAttendeeCommand request,
      CancellationToken             cancellationToken)
  {
    try
    {
      var activity = await _context.Activities.Include(a => a.Attendees)
                                   .SingleOrDefaultAsync(a => a.Id == request.ActivityId,
                                                         cancellationToken);

      if (activity == null) { throw new Exception("Activity not found."); }

      var attendee = activity.Attendees.SingleOrDefault(a => a.UserId == request.Id);
      var attendeeInfo = await _userService.GetUserInfoByIdAsync(request.Id);

      var hostUserId = activity.Attendees.FirstOrDefault(attendee => attendee.IsHost)!.UserId;
      var hostUser = await _userService.GetUserInfoByIdAsync(hostUserId);

      if (attendee == null)
      {
        var newAttendee = new Domain.Entities.ActivityAttendee
        {
            Activity = activity,
            UserId = request.Id,
            IsHost = false
        };

        await _context.ActivityAttendees.AddAsync(newAttendee,
                                                  cancellationToken);
      }
      else
      {
        if (attendeeInfo.UserName == hostUser.UserName)
        {
          activity.IsCancelled = !activity.IsCancelled;
        }
        else { _context.ActivityAttendees.Remove(attendee); }
      }

      var result = await _context.SaveChangesAsync(cancellationToken) > 0;

      return result
          ? Unit.Value
          : throw new Exception("Could not update activity attendee.");
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error saving to the database: {ExMessage}", ex.Message);

      throw;
    }
  }
}
