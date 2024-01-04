using Domain.ValueObjects.Activity;
using Domain.ValueObjects.ActivityAttendee;

namespace Domain.Events.Attendee;

public sealed class AttendeeRemovedDomainEvent : BaseEvent
{
  public AttendeeRemovedDomainEvent(ActivityId activityId, AttendeeId attendeeId)
  {
    ActivityId = activityId;
    AttendeeId = attendeeId;
  }

  public ActivityId ActivityId { get; init; }
  public AttendeeId AttendeeId { get; init; }
}
