using System;

using Application.common.Interfaces;
using Application.CQRS.Activities.Queries.GetAllAttendeeIdsByActivityIdQuery;
using Application.CQRS.Notifications.Commands;

using Domain.Enums;
using Domain.Events.Activity;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.EventHandlers;

public class
    ActivityCancelledDomainEventHandler : INotificationHandler<
    ActivityCanceledDomainEvent>
{
  private readonly IMediator                                    _mediator;
  private readonly INotificationService                         _notificationService;
  private readonly ILogger<ActivityCancelledDomainEventHandler> _logger;

  public ActivityCancelledDomainEventHandler(
      ILogger<ActivityCancelledDomainEventHandler> logger,
      INotificationService                         notificationService,
      IMediator                                    mediator)
  {
    _logger = logger;
    _notificationService = notificationService;
    _mediator = mediator;
  }

  public async Task Handle(
      ActivityCanceledDomainEvent notification,
      CancellationToken           cancellationToken)
  {
    _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

    var activityId = notification.ActivityId;
    var activityTitle = notification.Title;

    const string methodName = "ReceiveActivityCancelledMessage";

    var message = $"Activity {activityTitle} has been cancelled.";

    // get all user
    var userIds = await _mediator.Send(new GetAllAttendeeIdsByActivityIdQuery
                                 {
                                     ActivityId = activityId
                                 }, cancellationToken);

    // add UserNotification to database
    await _mediator.Send(new CreateNewNotificationCommand
    {
            Context = message,
            RelatedId = activityId.Value,
            NotificationType =
                    NotificationType.ActivityCanceled,
            UserIds = userIds
    }, cancellationToken);

    await _notificationService.SendActivityNotificationToAll(methodName,
      activityId.Value,
      activityId.Value,
      message);
  }
}
