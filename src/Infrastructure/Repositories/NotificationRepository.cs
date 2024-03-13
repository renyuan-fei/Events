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

  public async Task<Notification?> GetByIdAsync(NotificationId id)
  {
    return await DbContext.Notifications
                          .Where(x => x.Id == id)
                          .FirstOrDefaultAsync();
  }

  public async Task<Notification?> GetNotificationWithUserByIdAsync(NotificationId id)
  {
    return await DbContext.Notifications
                          .Where(x => x.Id == id)
                          .Include(x => x.UserNotifications)
                          .FirstOrDefaultAsync();
  }

  public IQueryable<Notification> GetUserNotificationQueryable(
      UserId         userId,
      DateTimeOffset initialTimestamp)
  {
    // use timestamp to Anchoring pagination
    return DbContext.Notifications.Where(notification => notification.UserNotifications
                                             .Any(userNotification =>
                                                      userNotification.UserId == userId
                                                   && notification.Created
                                                   <= initialTimestamp))
                    .Include(notification => notification.UserNotifications);
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
