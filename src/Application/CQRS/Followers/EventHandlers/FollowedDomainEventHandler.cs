using Domain.Events.Following;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Followers.EventHandlers;

public class FollowedDomainEventHandler : INotificationHandler<FollowedDomainEvent>
{
  private readonly ILogger<FollowedDomainEventHandler> _logger;

  public FollowedDomainEventHandler(ILogger<FollowedDomainEventHandler> logger)
  {
    _logger = logger;
  }

  public Task Handle(
      FollowedDomainEvent                 notification,
      CancellationToken cancellationToken)
  {
    _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

    //TODO

    return Task.CompletedTask;
  }
}

