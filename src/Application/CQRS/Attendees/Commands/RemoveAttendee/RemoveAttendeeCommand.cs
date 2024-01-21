using Application.Common.Helpers;
using Application.common.Interfaces;
using Application.Common.Interfaces;
using Application.common.Models;

using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using Domain.ValueObjects.Activity;
using Domain.ValueObjects.ActivityAttendee;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Attendees.Commands.DeleteAttendee;

public record RemoveAttendeeCommand : IRequest<Result>
{
  public string ActivityId { get; init; }
  public string UserId     { get; init; }
}

public class
    DeleteActivityAttendeeHandler : IRequestHandler<RemoveAttendeeCommand, Result>
{
  private readonly IUnitOfWork                            _unitOfWork;
  private readonly IActivityRepository                    _activityRepository;
  private readonly ILogger<DeleteActivityAttendeeHandler> _logger;

  public DeleteActivityAttendeeHandler(
      ILogger<DeleteActivityAttendeeHandler> logger,
      IActivityRepository                    activityRepository,
      IUnitOfWork                            unitOfWork)
  {
    _logger = logger;
    _activityRepository = activityRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task<Result> Handle(
      RemoveAttendeeCommand request,
      CancellationToken     cancellationToken)
  {
    try {
      var activityId = request.ActivityId;
      var userId = request.UserId;

      var activity = await _activityRepository.GetActivityWithAttendeesByIdAsync(new ActivityId(activityId),
          cancellationToken);

      GuardValidation.AgainstNull(activity, nameof(activity));

      var attendee = activity.Attendees.SingleOrDefault(attendee => attendee.Identity
          .UserId == new UserId(userId));

      GuardValidation.AgainstNull(attendee, $"attendee with id {userId} not found");

      if (attendee!.Identity.IsHost)
      {
        throw new InvalidOperationException("Host cannot be removed.");
      }

      activity.RemoveAttendee(new UserId(userId));

      var result = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

      if (!result)
      {
        throw new DbUpdateException("There was an error saving data to the database");
      }

      return Result.Success();
    }
    catch (Exception ex)
    {
      _logger.LogError(ex,
                       "Error occurred in {Name}: {ExMessage}",
                       nameof(RemoveAttendeeCommand),
                       ex
                           .Message);

      throw;
    }
  }
}
