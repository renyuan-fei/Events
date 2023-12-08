using Application.common.Interfaces;
using Application.Common.Interfaces;

using AutoMapper;

using Domain.Entities;

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
  private readonly IUserService                           _userService;
  private readonly IApplicationDbContext                  _context;
  private readonly IMapper                                _mapper;
  private readonly ILogger<UpdateActivityAttendeeHandler> _logger;

  public UpdateActivityAttendeeHandler(
      IApplicationDbContext                  context,
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

      var hostUser = activity.Attendees.FirstOrDefault(attendee => attendee.IsHost)
                             .UserName;

      if (attendee == null)
      {
        var currentUser = await _userService.GetUserInfoByIdAsync(request.Id);

        var newAttendee = new Domain.Entities.ActivityAttendee
        {
            Activity = activity,
            Bio = currentUser.Bio,
            DisplayName = currentUser.DisplayName,
            UserId = request.Id,
            IsHost = false
        };

        await _context.ActivityAttendees.AddAsync(newAttendee,
                                                  cancellationToken: cancellationToken);
      }
      else
      {
        if (attendee.UserName == hostUser)
        {
          activity.IsCancelled = !activity.IsCancelled;
        }
        else
        {
          _context.ActivityAttendees.Remove(attendee);
        }
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
