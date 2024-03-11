using Domain.ValueObjects.Message;

namespace Domain.Repositories;

public interface INotificationRepository
{
  Task AddAsync(Notification notification, CancellationToken cancellationToken = default);

  public IQueryable<Notification> GetUserNotificationQueryable(UserId userId);
  IQueryable<Notification> GetNotificationQueryable();
}
