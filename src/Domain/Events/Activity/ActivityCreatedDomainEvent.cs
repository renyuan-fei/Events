using Domain.ValueObjects.Activity;

namespace Domain.Events.Activity;

public sealed class ActivityCreatedDomainEvent : BaseEvent
{
  public ActivityCreatedDomainEvent(ActivityId activityId) { ActivityId = activityId; }

  public ActivityId ActivityId { get; private set; }
}
