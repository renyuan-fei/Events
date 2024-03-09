using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using Domain.ValueObjects.Message;

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

  public IQueryable<UserNotification> GetNotificationsByUserIdQueryable(UserId userId)
  {
    return DbContext.UserNotifications.Where(x => x.UserId == userId).AsQueryable();
  }

  public async Task<int> GetNotificationCountByUserIdAsync(UserId userId)
  {
    return await DbContext.UserNotifications.CountAsync(x => x.UserId == userId);
  }

}
