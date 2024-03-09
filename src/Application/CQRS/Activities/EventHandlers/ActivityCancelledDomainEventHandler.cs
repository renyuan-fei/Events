using System;
using Application.common.Interfaces;
using Domain.Events.Activity;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.EventHandlers;

public class ActivityCancelledDomainEventHandler : INotificationHandler<ActivityCanceledDomainEvent>
{
    private readonly INotificationService _notificationService;
    private readonly ILogger<ActivityCancelledDomainEventHandler> _logger;

    public ActivityCancelledDomainEventHandler(
        ILogger<ActivityCancelledDomainEventHandler> logger,
        INotificationService                         notificationService)
    {
        _logger = logger;
        _notificationService = notificationService;
    }

    public async Task Handle(
        ActivityCanceledDomainEvent notification,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

        var activityId = notification.ActivityId;
        var activityTitle = notification.Title;

        const string methodName = "ReceiveActivityCancelledMessage";

        var message = $"Activity {activityTitle} has been cancelled.";

        // add Notification to database

        // get all user

        // add UserNotification to database

        await _notificationService.SendActivityNotificationToAll(methodName,activityId.Value,
            activityId.Value, message);
    }
}