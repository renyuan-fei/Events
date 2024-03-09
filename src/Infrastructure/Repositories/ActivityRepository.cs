using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
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
           .Include(a => a.Attendees.OrderBy(attendee => attendee.Created))
           .FirstOrDefaultAsync(cancellationToken);
  }

  public async Task<List<Activity>> GetActivityByUserIdAsync(
      UserId            userId,
      CancellationToken cancellationToken = default)
  {
    return await DbContext.Activities.Where(activity => activity.Attendees.Any(attendee =>
                                                attendee.Identity.UserId == userId))
                          .Include(a => a.Attendees.OrderBy(attendee => attendee.Created))
                          .ToListAsync(cancellationToken);
  }

  public async Task<bool> IsActivityExistingAsync(
      ActivityId        id,
      CancellationToken cancellationToken = default)
  {
    return await DbContext.Activities.AnyAsync(activity => activity.Id == id,
                                               cancellationToken);
  }

  public IQueryable<Activity> GetAllActivitiesQueryable()
  {
    return DbContext.Activities
                    .Include(activity =>
                                 activity.Attendees.OrderBy(attendee => attendee.Created))
                    .OrderBy(activity => activity.Date)
                    .AsQueryable();
  }

  public async Task<bool> IsHostAsync(
      ActivityId activityId,
      UserId     userId,
      CancellationToken
          cancellationToken =
          default)
  {
    return await DbContext.Activities.Include(activity => activity.Attendees)
                          .FirstOrDefaultAsync(activity =>
                                                   activity.Id == activityId
                                                && activity.Attendees.Any(attendee =>
                                                       attendee.Identity.UserId
                                                    == userId
                                                    && attendee.Identity.IsHost),
                                               cancellationToken)
        != null;
  }

  public async Task<UserId?> GetHostIdAsync(ActivityId activityId)
  {
    return await DbContext.Activities
                               .Where(a => a.Id == activityId)
                               .SelectMany(a => a.Attendees)
                               .Where(a => a.Identity.IsHost)
                               .Select(a => a.Identity.UserId)
                               .SingleOrDefaultAsync();

  }
}
