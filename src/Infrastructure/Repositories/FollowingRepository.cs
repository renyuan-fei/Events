using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using Domain.ValueObjects.Following;

using Infrastructure.DatabaseContext;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class FollowingRepository : Repository<Following, FollowingId>,
                                   IFollowingRepository
{
  public FollowingRepository(EventsDbContext dbContext) : base(dbContext) { }

  public async Task<Following?> IsFollowingAsync(UserId followerId, UserId followingId)
  {
    return await DbContext.Followings.FirstOrDefaultAsync(
        f => f.Relationship.FollowerId == followerId &&
             f.Relationship.FollowingId == followingId);
  }

  public IQueryable<Following> GetFollowersByIdQueryable(UserId id)
  {
    return DbContext.Followings.Where(follow => follow.Relationship.FollowingId ==
        id).AsQueryable();
  }

  public IQueryable<Following> GetFollowingsByIdQueryable(UserId id)
  {
    return DbContext.Followings.Where(follow => follow.Relationship.FollowerId ==
        id).AsQueryable();
  }


}
