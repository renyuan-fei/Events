using Domain.Events.Following;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Followers.EventHandlers;

public class UnfollowedDomainEventHandler : INotificationHandler<UnfollowedDomainEvent>
{
  private readonly ILogger<UnfollowedDomainEventHandler> _logger;

  public UnfollowedDomainEventHandler(ILogger<UnfollowedDomainEventHandler> logger)
  {
    _logger = logger;
  }

  public Task Handle(
      UnfollowedDomainEvent                 notification,
      CancellationToken cancellationToken)
  {
    // TODO when user unfollow another user, let user leave the group of the user
    _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

    //TODO

    return Task.CompletedTask;
  }

}

