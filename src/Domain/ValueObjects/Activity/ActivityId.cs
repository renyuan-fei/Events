namespace Domain.ValueObjects.Activity;

public record ActivityId(Guid Value)
{
  public static ActivityId New() => new(Guid.NewGuid());
}
