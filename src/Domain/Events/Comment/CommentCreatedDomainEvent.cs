using Domain.Entities;
namespace Domain.Events.Comment;

public sealed class CommentCreateDomainEvent : BaseEvent
{
  public Entities.Comment Comment { get; private set; }
  public CommentCreateDomainEvent(Entities.Comment comment)
  {
    Comment = comment;
  }
}
