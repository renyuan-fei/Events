using System;

using Application.common.DTO;
using Application.common.Interfaces;
using Application.CQRS.Activities.Queries.GetActivityHostIdQuery;
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
  private readonly IMapper                                      _mapper;
  private readonly IMediator                                    _mediator;
  private readonly INotificationService                         _notificationService;
  private readonly ILogger<ActivityCancelledDomainEventHandler> _logger;

  public ActivityCancelledDomainEventHandler(
      ILogger<ActivityCancelledDomainEventHandler> logger,
      INotificationService                         notificationService,
      IMediator                                    mediator,
      IMapper                                      mapper)
  {
    _logger = logger;
    _notificationService = notificationService;
    _mediator = mediator;
    _mapper = mapper;
  }

  public async Task Handle(
      ActivityCanceledDomainEvent notification,
      CancellationToken           cancellationToken)
  {
    _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

    var activityId = notification.ActivityId;
    var activityTitle = notification.Title;

    const string methodName = "ReceiveNotificationMessage";

    var message = $"Activity {activityTitle} has been cancelled.";

    // get all user
    var userIds =
        await _mediator.Send(new GetAllAttendeeIdsByActivityIdQuery
                             {
                                 ActivityId = activityId
                             },
                             cancellationToken);

    var hostId = await _mediator.Send(new GetActivityHostIdQuery { activityId = activityId },
                                      cancellationToken);

    userIds.Remove(hostId);

    // no user notification
    if (userIds.Count == 0)
    {
        return;
    }

    // add UserNotification to database
    var newNotification = await _mediator.Send(new CreateNewNotificationCommand
                                               {
                                                   Context = message,
                                                   RelatedId = activityId.Value,
                                                   NotificationType = NotificationType.ActivityCanceled,
                                                   UserIds = userIds
                                               },
                                               cancellationToken);

    var notificationDto = _mapper.Map<NotificationDto>(newNotification);

    await _notificationService.SendActivityNotificationToAll(methodName,
      $"activity-{activityId.Value}",
      notificationDto);
  }
}
