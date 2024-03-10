using Domain.ValueObjects.Message;

namespace Domain.Entities;

public class UserNotification : BaseAuditableEntity<UserNotificationId>
{
  public UserId         UserId         { get; set; }
  public NotificationId NotificationId { get; set; }
  public bool           IsRead         { get; set; } = false;

  public Notification Notification { get; set; }

  private UserNotification() {}

  private UserNotification(
      UserNotificationId id,
      UserId         userId,
      bool           isRead,
      Notification   notification)
  {
    Id = id;
    UserId = userId;
    NotificationId = notification.Id;
    IsRead = isRead;
    Notification = notification;
  }

  public static UserNotification Create(
      UserId           userId,
      Notification notification,
      NotificationType type)
  {
    return new UserNotification(UserNotificationId.New(),userId, false, notification);
  }

  public void MarkAsRead() { IsRead = true; }

  public void MarkAsUnread() { IsRead = false; }
}
