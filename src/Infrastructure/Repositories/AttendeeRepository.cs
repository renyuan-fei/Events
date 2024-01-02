using System.Threading;
using System.Threading.Tasks;

using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects.Activity;
using Domain.ValueObjects.ActivityAttendee;

using Infrastructure.DatabaseContext;

namespace Infrastructure.Repositories;

public class AttendeeRepository : Repository<Attendee, AttendeeId>, IAttendeeRepository
{
  public AttendeeRepository(EventsDbContext dbContext) : base(dbContext) { }
}
