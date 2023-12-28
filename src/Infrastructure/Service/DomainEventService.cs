using Application.common.interfaces;
using Application.common.Models;

using Domain.Common;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Infrastructure.Service;

public class DomainEventService : IDomainEventService
{
  private readonly ILogger<DomainEventService> _logger;
  private readonly IPublisher                  _mediator;

  public DomainEventService(ILogger<DomainEventService> logger, IPublisher mediator)
  {
    _logger = logger;
    _mediator = mediator;
  }

  public async Task Publish(BaseEvent baseEvent)
  {
    _logger.LogInformation("Publishing domain event. Event - {event}",
                           baseEvent.GetType().Name);

    await _mediator.Publish(GetNotificationCorrespondingToDomainEvent(baseEvent));
  }

  private INotification GetNotificationCorrespondingToDomainEvent(BaseEvent baseEvent)
  {
    return (INotification)Activator.CreateInstance(typeof(DomainEventNotification<>)
                                                       .MakeGenericType(baseEvent
                                                           .GetType()),
                                                   baseEvent)!;
  }
}
