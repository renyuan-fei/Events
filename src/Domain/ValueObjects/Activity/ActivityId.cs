namespace Domain.ValueObjects.Activity;

public record ActivityId(string Value)
{
  public static ActivityId New() => new(Guid.NewGuid().ToString());
}
