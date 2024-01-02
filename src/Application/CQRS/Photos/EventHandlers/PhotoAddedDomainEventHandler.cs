using Application.common.Interfaces;
using Application.common.Models;

using Domain.Events.Photo;
using Domain.Repositories;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Photos.EventHandlers;

public class PhotoAddedDomainEventHandler : INotificationHandler<PhotoAddedDomainEvent>
{
  private readonly ILogger<PhotoAddedDomainEventHandler> _logger;

  public PhotoAddedDomainEventHandler(
      ILogger<PhotoAddedDomainEventHandler> logger)
  {
    _logger = logger;
  }

  public async Task Handle(
      PhotoAddedDomainEvent notification,
      CancellationToken     cancellationToken)
  {
    try
    {
      _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);
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
