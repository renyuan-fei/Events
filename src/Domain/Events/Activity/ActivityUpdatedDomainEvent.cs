namespace Domain.Events.Activity;

public sealed class ActivityUpdatedDomainEvent : BaseEvent
{
  public ActivityUpdatedDomainEvent(Entities.Activity activity)
  {
    this.activity = activity;
  }

  public Entities.Activity activity { get; private set; }
}
