using Domain.ValueObjects.Activity;

namespace Domain.Repositories;

public interface IActivityRepository
{
  Task AddAsync(Activity activity, CancellationToken cancellationToken = default);

  Task<List<UserId>> GetAllAttendeeIdsByActivityIdAsync(ActivityId activityId, CancellationToken cancellationToken = default);

  Task<Activity?> GetByIdAsync(
      ActivityId        id,
      CancellationToken cancellationToken = default);

  Task<Activity?> GetActivityWithAttendeesByIdAsync(
      ActivityId        id,
      CancellationToken cancellationToken = default);

  Task<List<Activity>> GetActivityByUserIdAsync(UserId userId, CancellationToken cancellationToken = default);

  Task<bool> IsActivityExistingAsync(ActivityId id, CancellationToken cancellationToken = default);

  IQueryable<Activity> GetAllActivitiesQueryable();

  public Task<bool> IsHostAsync(
          ActivityId activityId,
          UserId     userId,
          CancellationToken
                  cancellationToken =
                  default);

  public Task<UserId?> GetHostIdAsync(ActivityId activityId);

  void Remove(Activity activity);
}
