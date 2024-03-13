using Domain.ValueObjects.Message;

namespace Domain.Repositories;

public interface INotificationRepository
{
  public Task<Notification?> GetByIdAsync(NotificationId id);
  public Task<Notification?> GetNotificationWithUserByIdAsync(NotificationId id);

  Task AddAsync(Notification notification, CancellationToken cancellationToken = default);

  public IQueryable<Notification> GetUserNotificationQueryable(UserId userId, DateTimeOffset initialTimestamp);
  public IQueryable<Notification> GetUserNotificationQueryable(UserId userId);
  IQueryable<Notification> GetNotificationQueryable();
}
