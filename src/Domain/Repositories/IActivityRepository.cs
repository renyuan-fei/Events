using Domain.ValueObjects.Activity;

namespace Domain.Repositories;

public interface IActivityRepository
{
  Task AddAsync(Activity activity, CancellationToken cancellationToken = default);

  Task<Activity?> GetByIdAsync(
      ActivityId        id,
      CancellationToken cancellationToken = default);

  Task<Activity?> GetActivityWithAttendeesByIdAsync(
      ActivityId        id,
      CancellationToken cancellationToken = default);

  IQueryable<Activity> GetAllActivitiesWithAttendeesQueryable();

  public Task<bool> IsHostAsync(
          ActivityId activityId,
          UserId     userId,
          CancellationToken
                  cancellationToken =
                  default);

  void Remove(Activity activity);
}
