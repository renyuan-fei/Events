using Domain.ValueObjects.Activity;

namespace Domain.Events.Activity;

public sealed class  ActivityCreatedDomainEvent : BaseEvent
{
  public ActivityId ActivityId { get; private set; }

  public ActivityCreatedDomainEvent(ActivityId activityId)
  {
    ActivityId = activityId;
  }
}
