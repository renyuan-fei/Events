namespace Domain.Repositories;

public interface IAttendeeRepository
{
  Task AddAsync(Attendee attendee, CancellationToken cancellationToken = default);
}
