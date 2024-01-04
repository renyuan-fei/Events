using Domain.ValueObjects.Activity;

namespace Domain.Events.Activity;

public sealed class ActivityCanceledDomainEvent : BaseEvent
{
  public ActivityCanceledDomainEvent(ActivityId id) { ActivityId = id; }

  public ActivityId ActivityId { get; init; }
}
