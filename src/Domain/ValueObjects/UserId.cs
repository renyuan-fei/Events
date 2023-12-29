namespace Domain.ValueObjects;

public record UserId(string Value)
{
  public static UserId Create(string value) => new(value);
}
