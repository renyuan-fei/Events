using Domain.ValueObjects.Message;

namespace Domain.Entities;

public class Notification : BaseAuditableEntity<NotificationId>
{
  public string           Content { get; set; }
  public NotificationType Type    { get; set; }

  private Notification(NotificationId id, string content, NotificationType type)
  {
    Id = id;
    Content = content;
    Type = type;
  }

  public static Notification Create(string content, NotificationType type)
  {
    return new Notification(NotificationId.New(), content, type);
  }

  public void UpdateContent(string newContent) { Content = newContent; }
}
