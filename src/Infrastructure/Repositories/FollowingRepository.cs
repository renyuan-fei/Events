using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using Domain.ValueObjects.Following;

using Infrastructure.DatabaseContext;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class FollowingRepository : Repository<Follow, FollowId>,
                                   IFollowingRepository
{
  public FollowingRepository(EventsDbContext dbContext) : base(dbContext) { }

  public async Task<Follow?> IsFollowingAsync(UserId followerId, UserId followingId)
  {
    return await DbContext.Following.FirstOrDefaultAsync(
        f => f.Relationship.FollowerId == followerId &&
             f.Relationship.FollowingId == followingId);
  }

  public IQueryable<Follow> GetFollowersByIdQueryable(UserId id)
  {
    return DbContext.Following.Where(follow => follow.Relationship.FollowingId ==
        id).AsQueryable();
  }

  public IQueryable<Follow> GetFollowingsByIdQueryable(UserId id)
  {
    return DbContext.Following.Where(follow => follow.Relationship.FollowerId ==
        id).AsQueryable();
  }


}
