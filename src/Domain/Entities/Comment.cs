using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Domain.ValueObjects;
using Domain.ValueObjects.Activity;
using Domain.ValueObjects.Comment;

namespace Domain.Entities;

public class Comment : BaseAuditableEntity<CommentId>
{
  private Comment(UserId userId,string body, ActivityId activityId, Activity activity)
  {
    UserId = userId;
    Body = body;
    ActivityId = activityId;
    Activity = activity;
  }

  private Comment() { }

  public UserId     UserId     { get; private set; }
  public string     Body       { get; private set; }
  public ActivityId ActivityId { get; private set; }
  public Activity   Activity   { get; private set; }

  public Activity AddComment(ActivityId id, UserId userId, Comment comment)
  {
    throw new NotImplementedException();
  }

  public Activity RemoveComment(ActivityId id, Comment comment)
  {
    throw new NotImplementedException();
  }
}
