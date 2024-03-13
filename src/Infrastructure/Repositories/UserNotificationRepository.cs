using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using Domain.ValueObjects.Message;

using Infrastructure.Data.Migrations;
using Infrastructure.DatabaseContext;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserNotificationRepository: Repository<UserNotification, UserNotificationId>, IUserNotificationRepository
{
  public UserNotificationRepository(EventsDbContext dbContext) : base(dbContext)
  {
  }

  public async Task<UserNotification?> GetByIdAsync(UserNotificationId id)
  {
    return await DbContext.UserNotifications
             .Where(x => x.Id == id)
             .FirstOrDefaultAsync();
  }

  public IQueryable<UserNotification> GetNotificationsByUserIdQueryable(UserId userId,DateTimeOffset initialTimestamp)
  {
    return DbContext.UserNotifications.Where(x => x.UserId == userId && x.Created
     <= initialTimestamp).Include(user => user.Notification).AsQueryable();
  }

  public async Task<int> GetUnreadNotificationCountByUserIdAsync(UserId userId)
  {
    return await DbContext.UserNotifications.Where(userNotification => userNotification
        .UserId == userId && userNotification.IsRead == false).CountAsync();
  }
}
