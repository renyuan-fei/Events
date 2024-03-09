using Domain.ValueObjects.Activity;

namespace Domain.Events.Activity;

public sealed class ActivityCreatedDomainEvent : BaseEvent
{
  public ActivityCreatedDomainEvent(Entities.Activity activity) { Activity = activity; }

  public Entities.Activity Activity { get; private set; }
}
