using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects.Message;

using Infrastructure.DatabaseContext;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class NotificationRepository : Repository<Notification, NotificationId>, INotificationRepository
{
  public NotificationRepository(EventsDbContext dbContext) : base(dbContext) {}
}
