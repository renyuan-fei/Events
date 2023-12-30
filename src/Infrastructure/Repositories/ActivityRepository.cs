using Domain.Entities;
using Domain.ValueObjects.Activity;

using Infrastructure.DatabaseContext;

namespace Infrastructure.Repositories;

public class ActivityRepository : Repository<Activity,ActivityId>
{
  public ActivityRepository(EventsDbContext dbContext) : base(dbContext)
  {
  }
}
