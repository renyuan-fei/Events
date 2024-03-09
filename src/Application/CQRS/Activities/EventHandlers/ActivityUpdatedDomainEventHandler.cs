using Application.common.Interfaces;

using Domain.Events.Activity;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.EventHandlers;

public class
    ActivityUpdatedDomainEventHandler : INotificationHandler<ActivityUpdatedDomainEvent>
{
  private readonly INotificationService                       _notificationService;
  private readonly ILogger<ActivityUpdatedDomainEventHandler> _logger;

  public ActivityUpdatedDomainEventHandler(
      ILogger<ActivityUpdatedDomainEventHandler> logger,
      INotificationService                       notificationService)
  {
    _logger = logger;
    _notificationService = notificationService;
  }

  public async Task Handle(
      ActivityUpdatedDomainEvent notification,
      CancellationToken          cancellationToken)
  {
    _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

    var activityId = notification.ActivityId;
    var activityTitle = notification.Title;

    const string methodName = "ReceiveActivityUpdatedMessage";

    var message = $"Activity {activityTitle} was updated.";

    // add Notification to database

    // get all user

    // add UserNotification to database

    await _notificationService.SendActivityNotificationToAll(methodName,
      activityId.Value,
      activityId.Value,
      message);
  }
}
