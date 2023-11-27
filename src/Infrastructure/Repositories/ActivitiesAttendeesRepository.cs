using Domain.Entities;
using Domain.RepositoryContacts;

using Infrastructure.DatabaseContext;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories;

public class ActivitiesAttendeesRepository : IActivityAttendeesRepository
{
  private readonly ApplicationDbContext                   _db;
  private readonly ILogger<ActivitiesAttendeesRepository> _logger;

  public ActivitiesAttendeesRepository(
      ILogger<ActivitiesAttendeesRepository> logger,
      ApplicationDbContext                   db)
  {
    _logger = logger;
    _db = db;
  }

  public async Task<ActivityAttendee> AddActivityAttendee(
      ActivityAttendee activityAttendee)
  {
    await _db.ActivityAttendees.AddAsync(activityAttendee);
    await _db.SaveChangesAsync();

    return activityAttendee;
  }

  public async Task<List<ActivityAttendee>> GetAllActivityAttendees()
  {
    return await _db.ActivityAttendees.OrderBy(attendees => attendees.AppUser.UserName)
                    .ToListAsync();
  }

  public async Task<ActivityAttendee?> GetActivityAttendeeByActivityAttendeeId(Guid id)
  {
    var activityAttendee = await _db.ActivityAttendees.FirstOrDefaultAsync(attendees =>
        attendees.Id == id);

    return activityAttendee;
  }

  public async Task<bool> DeleteActivityAttendeeByActivityAttendeeId(Guid id)
  {
    if (_db.ActivityAttendees == null) return false;

    var activityAttendee = await _db.ActivityAttendees.FirstOrDefaultAsync(attendees =>
        attendees.Id == id);

    if (activityAttendee != null) _db.ActivityAttendees.Remove(activityAttendee);

    return await _db.SaveChangesAsync() > 0;
  }

  public async Task<ActivityAttendee> UpdateActivityAttendee(
      ActivityAttendee
          activityAttendee)
  {
    var activityAttendeeToUpdate =
        await _db.ActivityAttendees.FirstOrDefaultAsync(attendees =>
                                                            attendees.Id
                                                         == activityAttendee.Id);

    if (activityAttendeeToUpdate == new ActivityAttendee()
     || activityAttendeeToUpdate == null)
    {
      return
          activityAttendee;
    }

    activityAttendeeToUpdate.IsHost = activityAttendee.IsHost;
    activityAttendeeToUpdate.AppUser = activityAttendee.AppUser;

    await _db.SaveChangesAsync();

    return activityAttendeeToUpdate;
  }
}
