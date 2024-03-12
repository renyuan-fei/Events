using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using Domain.ValueObjects.Message;

using Infrastructure.Data.Migrations;
using Infrastructure.DatabaseContext;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class NotificationRepository : Repository<Notification, NotificationId>,
                                      INotificationRepository
{
  public NotificationRepository(EventsDbContext dbContext) : base(dbContext) { }

  public IQueryable<Notification> GetUserNotificationQueryable(
      UserId   userId,
      DateTimeOffset initialTimestamp)
  {
    // use timestamp to Anchoring pagination
    return DbContext.Notifications.Where(notification => notification.UserNotifications
                                             .Any(userNotification =>
                                                      userNotification.UserId == userId
                                                   && notification.Created
                                                   <= initialTimestamp));
  }

  public IQueryable<Notification> GetUserNotificationQueryable(UserId userId)
  {
    return DbContext.Notifications.Where(notification => notification.UserNotifications
                                             .Any(userNotification =>
                                                      userNotification.UserId == userId));
  }

  public IQueryable<Notification> GetNotificationQueryable()
  {
    return DbContext.Notifications.AsQueryable();
  }
}
