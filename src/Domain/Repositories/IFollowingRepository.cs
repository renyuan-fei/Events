namespace Domain.Repositories;

public interface IFollowingRepository
{
  public Task<Follow?> IsFollowingAsync(UserId followerId, UserId followingId);

  IQueryable<Follow> GetFollowersByIdQueryable(UserId id);

  IQueryable<Follow> GetFollowingsByIdQueryable(UserId id);

  public Task AddAsync(Follow entity, CancellationToken cancellationToken = default);

  public void Remove(Follow entity);
}
