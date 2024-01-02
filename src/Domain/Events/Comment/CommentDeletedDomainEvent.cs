using Domain.ValueObjects.Activity;
using Domain.ValueObjects.Comment;

namespace Domain.Events.Comment;

public sealed class CommentDeletedDomainEvent : BaseEvent
{
  public ActivityId ActivityId { get; private set; }

  public CommentId CommentId  { get; private set; }

  public CommentDeletedDomainEvent(ActivityId activityId, CommentId commentId)
  {
    this.ActivityId = activityId;
    this.CommentId = commentId;
  }
}
