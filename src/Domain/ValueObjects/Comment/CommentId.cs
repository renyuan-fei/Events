namespace Domain.ValueObjects.Comment;

public record CommentId(string Value)
{
  public static CommentId New() => new(Guid.NewGuid().ToString());
}
