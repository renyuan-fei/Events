using Application.Common.Helpers;
using Application.common.Interfaces;
using Application.common.Models;

using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using Domain.ValueObjects.Activity;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Attendees.Commands.AddAttendee;

public record AddAttendeeCommand : IRequest<Result>
{
  public string ActivityId { get; init; }
  public string UserId { get; init; }
}

public class
    CreateActivityAttendeeHandler : IRequestHandler<AddAttendeeCommand, Result>
{
  private readonly IUnitOfWork                            _unitOfWork;
  private readonly IActivityRepository                    _activityRepository;
  private readonly ILogger<CreateActivityAttendeeHandler> _logger;

  public CreateActivityAttendeeHandler(
      ILogger<CreateActivityAttendeeHandler> logger,
      IActivityRepository                    activityRepository,
      IUnitOfWork                            unitOfWork)
  {
    _logger = logger;
    _activityRepository = activityRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task<Result> Handle(
      AddAttendeeCommand request,
      CancellationToken     cancellationToken)
  {
    try
    {
      var activityId = request.ActivityId;
      var userId = request.UserId;

      var activity = await _activityRepository.GetActivityWithAttendeesByIdAsync(new ActivityId(activityId),
          cancellationToken);

      GuardValidation.AgainstNull(activity, nameof(activity));

      var isExisting = activity.Attendees.Any(attendee => attendee.Identity.UserId == new UserId(userId));

      if (isExisting)
      {
        throw new InvalidOperationException("Attendee already exists.");
      }

      var attendee = Attendee.Create(new UserId(userId),false,new ActivityId(activityId), activity);

      activity.AddAttendee(attendee);

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
                       nameof(AddAttendeeCommand),
                       ex
                           .Message);

      throw;
    }
  }
}
