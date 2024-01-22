namespace Domain.Events.Comment;

public sealed class CommentAddedDomainEvent : BaseEvent
{
  public CommentAddedDomainEvent(Entities.Comment comment) { Comment = comment; }

  public Entities.Comment Comment { get; private set; }
}
