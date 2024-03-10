using Domain.ValueObjects.Message;

using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities;

public class Notification : BaseAuditableEntity<NotificationId>
{
  public string           Content { get; private set; }
  public NotificationType Type    { get; private set; }

  public string RelatedId { get; private set; }

  private readonly List<UserNotification> _userNotifications = new List<UserNotification>();

  public IReadOnlyCollection<UserNotification> UserNotifications => _userNotifications.AsReadOnly();

  private Notification() { }

  private Notification(
      NotificationId   id,
      string           relatedId,
      string           content,
      NotificationType type)
  {
    Id = id;
    Content = content;
    Type = type;
    RelatedId = relatedId;
  }

  public static Notification Create(
      string           content,
      string           relatedId,
      NotificationType type)
  {
    return new Notification(NotificationId.New(), relatedId, content, type);
  }

  public void UpdateContent(string newContent) { Content = newContent; }

  // 创建并添加一个UserNotification
  public UserNotification AddUserNotification(UserId userId)
  {
    var userNotification = UserNotification.Create(userId, this, this.Type);
    _userNotifications.Add(userNotification);

    return userNotification;
  }

  // 批量添加UserNotifications
  public IEnumerable<UserNotification> AddUserNotifications(IEnumerable<UserId> userIds)
  {
    var userNotifications = userIds.Select(userId => UserNotification
                                               .Create(userId, this, this.Type))
                                   .ToList();

    _userNotifications.AddRange(userNotifications);

    return userNotifications;
  }
}
