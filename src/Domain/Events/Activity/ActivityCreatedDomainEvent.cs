using Domain.ValueObjects.Activity;

namespace Domain.Events.Activity;

public sealed class  ActivityCreatedDomainEvent : BaseEvent
{
  public ActivityId Id { get; private set; }

  public ActivityCreatedDomainEvent(ActivityId id) { Id = id; }
}
