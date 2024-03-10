using Application.common.Interfaces;
using Application.CQRS.Activities.Queries.GetAllAttendeeIdsByActivityIdQuery;
using Application.CQRS.Notifications.Commands;

using Domain.Enums;
using Domain.Events.Activity;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.EventHandlers;

public class
    ActivityUpdatedDomainEventHandler : INotificationHandler<ActivityUpdatedDomainEvent>
{
  private readonly IMediator _mediator;
  private readonly INotificationService                       _notificationService;
  private readonly ILogger<ActivityUpdatedDomainEventHandler> _logger;

  public ActivityUpdatedDomainEventHandler(
      ILogger<ActivityUpdatedDomainEventHandler> logger,
      INotificationService                       notificationService,
      IMediator                                  mediator)
  {
    _logger = logger;
    _notificationService = notificationService;
    _mediator = mediator;
  }

  public async Task Handle(
      ActivityUpdatedDomainEvent notification,
      CancellationToken          cancellationToken)
  {
    _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

    var activityId = notification.ActivityId;
    var activityTitle = notification.Title;

    const string methodName = "ReceiveNotificationMessage";

    var message = $"Activity {activityTitle} was updated.";

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
        NotificationType = NotificationType.ActivityUpdated,
        UserIds = userIds
    }, cancellationToken);

    await _notificationService.SendActivityNotificationToAll(methodName,
      $"activity-{activityId.Value}",
      activityId.Value,
      message);
  }
}
