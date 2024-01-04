namespace Domain.ValueObjects.Comment;

public record CommentId(string Value)
{
  public static CommentId New() { return new CommentId(Guid.NewGuid().ToString()); }
}
