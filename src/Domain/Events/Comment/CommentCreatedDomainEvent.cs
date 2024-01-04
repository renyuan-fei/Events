namespace Domain.Events.Comment;

public sealed class CommentCreateDomainEvent : BaseEvent
{
  public CommentCreateDomainEvent(Entities.Comment comment) { Comment = comment; }

  public Entities.Comment Comment { get; private set; }
}
