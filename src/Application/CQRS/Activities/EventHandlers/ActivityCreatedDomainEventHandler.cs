using Application.Common.Helpers;
using Application.common.Interfaces;
using Application.CQRS.Activities.Queries.GetActivityHostIdQuery;
using Application.CQRS.Followers.Queries;
using Application.CQRS.Notifications.Commands;

using Domain.Enums;
using Domain.Events.Activity;
using Domain.ValueObjects;

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

    var userId = activity.Attendees.FirstOrDefault(attendee => attendee.Identity.IsHost)?.Identity.UserId;

    GuardValidation.AgainstNull(userId, "host user not found");

    var host = await _userService.GetUserByIdAsync(userId!.Value, cancellationToken);

    const string methodName = "ReceiveActivityCreatedMessage";

    var message =
        $"New Activity Alert: '{activity.Title}' is scheduled for {activity.Date:yyyy-MM-dd HH:mm} at '{activity.Location}'. "
      + $"Brief: {activity.Description}. Hosted by {host?.DisplayName}.";

    // get all user
    var userIds = await _mediator.Send(new GetFollowersIdQuery
    {
        UserId = userId.Value
    }, cancellationToken);

    // add UserNotification to database
    await _mediator.Send(new CreateNewNotificationCommand
    {
            Context = message,
            RelatedId = activity.Id.Value,
            NotificationType = NotificationType.ActivityCreated,
            UserIds = userIds.Select(id => new UserId(id)).ToList()
    }, cancellationToken);

    await _notificationService.SendActivityNotificationToAll(methodName,
      userId.Value,
      activity.Id.Value,
      message);
  }
}
