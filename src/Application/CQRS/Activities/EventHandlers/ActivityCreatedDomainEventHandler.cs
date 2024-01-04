using Domain.Events.Activity;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.EventHandlers;

public class
    ActivityCreatedDomainEventHandler : INotificationHandler<ActivityCreatedDomainEvent>
{
  private readonly ILogger<ActivityCreatedDomainEventHandler> _logger;

  public ActivityCreatedDomainEventHandler(
      ILogger<ActivityCreatedDomainEventHandler> logger)
  {
    _logger = logger;
  }

  public Task Handle(
      ActivityCreatedDomainEvent notification,
      CancellationToken          cancellationToken)
  {
    _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

    return Task.CompletedTask;
  }
}
