using Domain.ValueObjects.Activity;

namespace Domain.Events.Activity;

public sealed class ActivityUpdatedDomainEvent : BaseEvent
{
  public ActivityUpdatedDomainEvent(ActivityId id, string title)
  {
    ActivityId = id;
    Title = title;
  }

  public ActivityId ActivityId { get; }
  public string Title { get; set; }
}
