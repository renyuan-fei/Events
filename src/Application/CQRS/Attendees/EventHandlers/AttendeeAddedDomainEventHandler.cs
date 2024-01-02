using Application.common.Interfaces;

using Domain.Events.Attendee;
using Domain.Repositories;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Attendees.EventHandlers;

public class
    AttendeeAddedDomainEventHandler : INotificationHandler<AttendeeAddedDomainEvent>
{
  private readonly IAttendeeRepository                        _attendeeRepository;
  private readonly IUnitOfWork                                _unitOfWork;
  private readonly ILogger<AttendeeAddedDomainEventHandler> _logger;

  public AttendeeAddedDomainEventHandler(
      ILogger<AttendeeAddedDomainEventHandler> logger,
      IAttendeeRepository                        attendeeRepository,
      IUnitOfWork                                unitOfWork)
  {
    _logger = logger;
    _attendeeRepository = attendeeRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task Handle(
      AttendeeAddedDomainEvent notification,
      CancellationToken         cancellationToken)
  {
    try

    {
      _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

      await _attendeeRepository.AddAsync(notification.Attendee, cancellationToken);

      var result = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

      _logger.LogInformation("Attendee operation result in repository: {Result}, Attendee ID: {AttendeeId}",
                             result
                                 ? "added"
                                 : "not added",
                             notification.Attendee.Id);
    }
    catch (Exception e)
    {
      _logger.LogError(e,
                       "Error occurred while handling {DomainEvent}",
                       notification.GetType().Name);

      throw;
    }
  }
}
