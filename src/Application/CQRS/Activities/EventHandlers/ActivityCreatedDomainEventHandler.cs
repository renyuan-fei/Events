using Application.common.Interfaces;
using Application.CQRS.Activities.Queries.GetActivityHostIdQuery;

using Domain.Events.Activity;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.EventHandlers;

public class
    ActivityCreatedDomainEventHandler : INotificationHandler<ActivityCreatedDomainEvent>
{
  private readonly IUserService                               _userService;
  private readonly IMediator                                  _mediator;
  private readonly INotificationService                       _notificationService;
  private readonly ILogger<ActivityCreatedDomainEventHandler> _logger;

  public ActivityCreatedDomainEventHandler(
      ILogger<ActivityCreatedDomainEventHandler> logger,
      INotificationService                       notificationService,
      IMediator                                  mediator,
      IUserService                               userService)
  {
    _logger = logger;
    _notificationService = notificationService;
    _mediator = mediator;
    _userService = userService;
  }

  public async Task Handle(
      ActivityCreatedDomainEvent notification,
      CancellationToken          cancellationToken)
  {
    _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

    var activity = notification.Activity;

    var userId = await _mediator.Send(new GetActivityHostIdQuery
                                      {
                                          activityId = activity.Id
                                      },
                                      cancellationToken);

    var host = await _userService.GetUserByIdAsync(userId.Value, cancellationToken);

    const string methodName = "ReceiveActivityCreatedMessage";

    var message =
        $"New Activity Alert: '{activity.Title}' is scheduled for {activity.Date:yyyy-MM-dd HH:mm} at '{activity.Location}'. "
      + $"Brief: {activity.Description}. Hosted by {host?.DisplayName}.";

    // add Notification to database

    // get all user

    // add UserNotification to database

    await _notificationService.SendActivityNotificationToAll(methodName,
      userId.Value,
      activity.Id.Value,
      message);
  }
}
