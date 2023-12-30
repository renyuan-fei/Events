using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects.Activity;

using Infrastructure.DatabaseContext;

namespace Infrastructure.Repositories;

public class AttendeeRepository : Repository<Activity,ActivityId>, IAttendeeRepository
{
  public AttendeeRepository(EventsDbContext dbContext) : base(dbContext)
  {
  }
}
