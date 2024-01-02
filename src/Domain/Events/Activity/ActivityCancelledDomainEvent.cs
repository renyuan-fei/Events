using Domain.ValueObjects.Activity;

namespace Domain.Events.Activity;

public sealed class ActivityCanceledDomainEvent : BaseEvent
{
  public ActivityId ActivityId { get; init; }

  public ActivityCanceledDomainEvent(ActivityId id) { ActivityId = id; }
}
