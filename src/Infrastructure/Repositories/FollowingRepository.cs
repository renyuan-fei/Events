using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects.Following;

using Infrastructure.DatabaseContext;

namespace Infrastructure.Repositories;

public class FollowingRepository : Repository<Following, FollowingId> , IFollowingRepository
{
  public FollowingRepository(EventsDbContext dbContext) : base(dbContext)
  {
  }
}
