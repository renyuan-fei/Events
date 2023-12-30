using Domain.ValueObjects.Activity;

namespace Domain.Events.Activity;

public sealed class  CreateActivityDomainEvent : BaseEvent
{
  public ActivityId Id { get; private set; }

  public CreateActivityDomainEvent(ActivityId id) { Id = id; }
}
