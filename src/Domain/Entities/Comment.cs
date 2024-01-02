using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Domain.Events.Comment;
using Domain.ValueObjects;
using Domain.ValueObjects.Activity;
using Domain.ValueObjects.Comment;

namespace Domain.Entities;

public class Comment : BaseAuditableEntity<CommentId>
{
  private Comment(UserId userId,string body, ActivityId activityId)
  {
    UserId = userId;
    Body = body;
    ActivityId = activityId;
  }

  private Comment() { }

  public UserId     UserId     { get; private set; }
  public string     Body       { get; private set; }
  public ActivityId ActivityId { get; private set; }
  public Activity   Activity   { get; private set; }

  public void AddComment(ActivityId id, UserId userId, string body)
  {
    var newComment = new Comment(userId, body, id);

    AddDomainEvent(new CommentCreateDomainEvent(newComment));
  }

  public void RemoveComment(ActivityId id, CommentId commentId)
  {
    AddDomainEvent(new CommentDeletedDomainEvent(id,commentId));
  }
}
