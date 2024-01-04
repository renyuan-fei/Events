using Domain.Events.Activity;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.EventHandlers;

public class
    ActivityCancelledDomainEventHandler : INotificationHandler<
    ActivityCanceledDomainEvent>
{
  private readonly ILogger<ActivityCancelledDomainEventHandler> _logger;

  public ActivityCancelledDomainEventHandler(
      ILogger<ActivityCancelledDomainEventHandler> logger)
  {
    _logger = logger;
  }

  public Task Handle(
      ActivityCanceledDomainEvent notification,
      CancellationToken           cancellationToken)
  {
    _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

    return Task.CompletedTask;
  }
}
