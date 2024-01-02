using Domain.Enums;
using Domain.ValueObjects.Activity;

namespace Domain.Events.Activity;

public sealed class ActivitySetCategoryDomainEvent : BaseEvent
{
  public ActivityId id { get; init; }
  public Category category { get; init; }

  public ActivitySetCategoryDomainEvent(ActivityId id, Category category)
  {
    this.id = id;
    this.category = category;
  }
}
