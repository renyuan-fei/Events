using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Domain.ValueObjects;
using Domain.ValueObjects.Activity;
using Domain.ValueObjects.Comment;

namespace Domain.Entities;

public class Comment : BaseAuditableEntity<CommentId>
{
  private Comment(string body, ActivityId activityId, Activity activity)
  {
    Body = body;
    ActivityId = activityId;
    Activity = activity;
  }

  private Comment() {
  }

  public string     Body       { get; private set; }

  public ActivityId ActivityId { get; private set; }
  public Activity   Activity   { get; private set; }
}
