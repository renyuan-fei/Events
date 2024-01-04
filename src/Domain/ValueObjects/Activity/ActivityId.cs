namespace Domain.ValueObjects.Activity;

public record ActivityId(string Value)
{
  public static ActivityId New() { return new ActivityId(Guid.NewGuid().ToString()); }
}
