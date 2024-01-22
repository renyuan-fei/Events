using Domain.ValueObjects.Activity;
using Domain.ValueObjects.Comment;

namespace Domain.Events.Comment;

public sealed class CommentRemovedDomainEvent : BaseEvent
{
  public CommentRemovedDomainEvent(ActivityId activityId, CommentId commentId)
  {
    ActivityId = activityId;
    CommentId = commentId;
  }

  public ActivityId ActivityId { get; private set; }

  public CommentId CommentId { get; private set; }
}
