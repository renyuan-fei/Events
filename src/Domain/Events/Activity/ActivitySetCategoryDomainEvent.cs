using Domain.ValueObjects.Activity;

namespace Domain.Events.Activity;

public sealed class ActivitySetCategoryDomainEvent : BaseEvent
{
  public ActivitySetCategoryDomainEvent(ActivityId id, Category category)
  {
    this.id = id;
    this.category = category;
  }

  public ActivityId id       { get; init; }
  public Category   category { get; init; }
}
