using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Application.common.Models;

using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects.Activity;

using Infrastructure.DatabaseContext;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ActivityRepository : Repository<Activity, ActivityId>, IActivityRepository
{
  public ActivityRepository(EventsDbContext dbContext) : base(dbContext) { }

  public Task<Activity?> GetActivityWithAttendeesByIdAsync(
      ActivityId        id,
      CancellationToken cancellationToken = default)
  {
    return DbContext
           .Activities
           .Where(activity => activity.Id == id)
           .Include(a => a.Attendees)
           .FirstOrDefaultAsync(cancellationToken);
  }

  public IQueryable<Activity> GetAllActivitiesWithAttendeesQueryable()
  {
    return DbContext.Activities.Include(activity => activity.Attendees).AsQueryable();
  }
}
