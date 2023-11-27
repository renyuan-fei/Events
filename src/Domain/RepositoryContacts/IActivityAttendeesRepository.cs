using Domain.Entities;

namespace Domain.RepositoryContacts;

/// <summary>
/// CRUD operations for ActivityAttendees.
/// </summary>
public interface IActivityAttendeesRepository
{
  /// <summary>
  /// Add a new activity attendee to the database.
  /// </summary>
  /// <param name="activityAttendee"></param>
  /// <returns></returns>
  public Task<ActivityAttendee> AddActivityAttendee(ActivityAttendee activityAttendee);

  /// <summary>
  /// Get all activity attendees from the database.
  /// </summary>
  /// <returns></returns>
  public Task<List<ActivityAttendee>> GetAllActivityAttendees();

  /// <summary>
  /// Get a specific activity attendee from the database by its id.
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  public Task<ActivityAttendee?> GetActivityAttendeeByActivityAttendeeId(Guid id);

  /// <summary>
  /// Deletes a specific activity attendee from the database by its id.
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  public Task<bool> DeleteActivityAttendeeByActivityAttendeeId(Guid id);

  /// <summary>
  /// Updates a specific activity attendee from the database.
  /// </summary>
  /// <param name="activityAttendee"></param>
  /// <returns></returns>
  public Task<ActivityAttendee> UpdateActivityAttendee(ActivityAttendee activityAttendee);
}
