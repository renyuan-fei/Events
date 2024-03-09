using Domain.ValueObjects.Message;

namespace Domain.Entities;

public class UserNotification: BaseAuditableEntity<UserNotificationId>
{
  public UserId         UserId         { get; set; }
  public NotificationId NotificationId { get; set; }
  public bool           IsRead         { get; set; } = false;

  public Notification Notification { get; set; }

  private UserNotification(UserId userId, NotificationId notificationId, bool isRead,
      Notification notification)
  {
    UserId = userId;
    NotificationId = notificationId;
    IsRead = isRead;
    Notification = notification;
  }

  public UserNotification Create(UserId userId, string content, NotificationType type)
  {
    var notification = Notification.Create(content, type);
    return new UserNotification(userId, notification.Id, false, notification);
  }

  public void MarkAsRead()
  {
    IsRead = true;
  }

  public void MarkAsUnread()
  {
    IsRead = false;
  }

}
