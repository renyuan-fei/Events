using Domain.Entities;

namespace Domain.RepositoryContacts;

/// <summary>
/// CRUD operations for Activities.
/// </summary>
public interface IActivitiesRepository
{
  /// <summary>
  /// Add a new activity to the database.
  /// </summary>
  /// <param name="activity"></param>
  /// <returns></returns>
  public Task<Activity> AddActivity(Activity activity);

  /// <summary>
  /// Get all activities from the database.
  /// </summary>
  /// <returns></returns>
  public Task<List<Activity>> GetAllActivities();

  /// <summary>
  /// Get a specific activity from the database by its id.
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  public Task<Activity?> GetActivityByActivityId(Guid id);

  /// <summary>
  /// Get a specific activity from the database by its name.
  /// </summary>
  /// <param name="title"></param>
  /// <returns></returns>
  public Task<Activity?> GetActivityByTitle(string title);

  /// <summary>
  /// Deletes a specific activity from the database by its id.
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  public Task<bool> DeleteActivityByActivityId(Guid id);

  /// <summary>
  /// Updates a specific activity from the database.
  /// </summary>
  /// <param name="activity"></param>
  /// <returns></returns>
  public Task<Activity> UpdateActivity(Activity activity);
}
