namespace Domain.Repositories;

public interface IFollowingRepository
{
  public Task<Following?> IsFollowingAsync(UserId followerId, UserId followingId);

  IQueryable<Following> GetFollowersByIdQueryable(UserId id);

  IQueryable<Following> GetFollowingsByIdQueryable(UserId id);

  public Task AddAsync(Following entity, CancellationToken cancellationToken = default);

  public void Remove(Following entity);
}
