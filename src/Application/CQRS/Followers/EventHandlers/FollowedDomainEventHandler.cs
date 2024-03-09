using Application.Common.Helpers;
using Application.common.Interfaces;

using Domain.Events.Following;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Followers.EventHandlers;

public class FollowedDomainEventHandler : INotificationHandler<FollowedDomainEvent>
{
  private readonly IUserService                        _userService;
  private readonly INotificationService                _notificationService;
  private readonly ILogger<FollowedDomainEventHandler> _logger;

  public FollowedDomainEventHandler(
      ILogger<FollowedDomainEventHandler> logger,
      INotificationService                notificationService,
      IUserService                        userService)
  {
    _logger = logger;
    _notificationService = notificationService;
    _userService = userService;
  }

  public async Task Handle(
      FollowedDomainEvent notification,
      CancellationToken   cancellationToken)
  {
    _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

    var followingInfo = notification.Following;
    var followerId = followingInfo.Relationship.FollowerId;
    var followingId = followingInfo.Relationship.FollowingId;

    var follower = await _userService.GetUserByIdAsync(followerId.Value,cancellationToken);

    GuardValidation.AgainstNull(follower, $"User with id {followerId} not found", followerId
        .Value);

    const string methodName = "ReceivedNewFollowerNotification";

    var message = $"Dear user, you have a new follower: {follower!.DisplayName}.";

    // add Notification to database

    // get all user

    // add UserNotification to database

    await _notificationService.SendMessageToUser(methodName,
                                                 followingId.Value,
                                                 followerId.Value,
                                                 message);
  }
}