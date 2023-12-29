namespace Domain.ValueObjects;

public record UserId(Guid Value)
{
  public static UserId Create(Guid value) => new(value);
}
