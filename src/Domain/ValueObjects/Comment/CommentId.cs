namespace Domain.ValueObjects;

public record CommentId(Guid Value)
{
  public static CommentId New() => new(Guid.NewGuid());
}
