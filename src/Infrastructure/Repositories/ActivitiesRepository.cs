using Domain.Entities;
using Domain.RepositoryContacts;

using Infrastructure.DatabaseContext;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories;

public class ActivitiesRepository : IActivitiesRepository
{
  private readonly ApplicationDbContext          _db;
  private readonly ILogger<ActivitiesRepository> _logger;

  public ActivitiesRepository(
      ApplicationDbContext          db,
      ILogger<ActivitiesRepository> logger)
  {
    _db = db;
    _logger = logger;
  }

  public async Task<Activity> AddActivity(Activity activity)
  {
    await _db.Activities.AddAsync(activity);
    await _db.SaveChangesAsync();

    return activity;
  }

  public async Task<List<Activity>> GetAllActivities()
  {
    if (_db.Activities != null)
      return await _db.Activities
                      .OrderBy(activities => activities.Date)
                      .ToListAsync();

    return new List<Activity>();
  }

  public async Task<Activity?> GetActivityByActivityId(Guid id)
  {
    return await _db.Activities.FirstOrDefaultAsync(activities => activities.Id == id);
  }

  public async Task<Activity?> GetActivityByTitle(string title)
  {
    return await _db.Activities.FirstOrDefaultAsync(activities =>
                                                        activities.Title == title);
  }

  public async Task<bool> DeleteActivityByActivityId(Guid id)
  {
    var activity =
        await _db.Activities.FirstOrDefaultAsync(activities => activities.Id == id);

    if (activity == null) { return false; }

    _db.Activities.Remove(activity);

    return await _db.SaveChangesAsync() > 0;
  }

  public async Task<Activity> UpdateActivity(Activity activity)
  {
    var activityToUpdate =
        await _db.Activities.FirstOrDefaultAsync(activities =>
                                                     activities.Id == activity.Id);

    if (activityToUpdate == null) { return activity; }

    activityToUpdate.Title = activity.Title;
    activityToUpdate.Category = activity.Category;
    activityToUpdate.Description = activity.Description;
    activityToUpdate.City = activity.City;
    activityToUpdate.Venue = activity.Venue;
    activityToUpdate.Date = activity.Date;

    await _db.SaveChangesAsync();

    return activityToUpdate;
  }
}
