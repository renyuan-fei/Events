using Domain.ValueObjects.Message;

namespace Domain.Repositories;

public interface IUserNotificationRepository
{
  public Task<UserNotification?> GetByIdAsync(UserNotificationId id);

  public IQueryable<UserNotification> GetNotificationsByUserIdQueryable(UserId userId);

  Task<int> GetNotificationCountByUserIdAsync(UserId userId);
}
