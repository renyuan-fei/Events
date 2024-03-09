using Domain.ValueObjects.Activity;

namespace Domain.Events.Activity;

public sealed class ActivityCanceledDomainEvent : BaseEvent
{
  public ActivityCanceledDomainEvent(ActivityId id, string title)
  {
    ActivityId = id;
    Title = title;
  }

  public ActivityId ActivityId { get; init; }
  public string     Title      { get; init; }
}
