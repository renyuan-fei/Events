namespace Domain.Events.Activity;

public sealed class ActivityUpdatedDomainEvent : BaseEvent
{
  public Entities.Activity activity { get; private set; }

  public ActivityUpdatedDomainEvent(Entities.Activity activity) { this.activity = activity; }
}
