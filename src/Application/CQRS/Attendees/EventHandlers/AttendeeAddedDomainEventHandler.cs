using Application.common.DTO;
using Application.Common.Helpers;
using Application.common.Interfaces;
using Application.CQRS.Notifications.Commands;

using Domain.Enums;
using Domain.Events.Attendee;
using Domain.ValueObjects;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Attendees.EventHandlers;

public class
    AttendeeAddedDomainEventHandler : INotificationHandler<AttendeeAddedDomainEvent>
{
  private readonly IMapper                                  _mapper;
  private readonly INotificationService                     _notificationService;
  private readonly IUserService                             _userService;
  private readonly IMediator                                _mediator;
  private readonly ILogger<AttendeeAddedDomainEventHandler> _logger;

  public AttendeeAddedDomainEventHandler(
      ILogger<AttendeeAddedDomainEventHandler> logger,
      IMediator                                mediator,
      IUserService                             userService,
      INotificationService                     notificationService,
      IMapper                                  mapper)
  {
    _logger = logger;
    _mediator = mediator;
    _userService = userService;
    _notificationService = notificationService;
    _mapper = mapper;
  }

  public async Task Handle(
      AttendeeAddedDomainEvent notification,
      CancellationToken        cancellationToken)
  {
    var attendee = notification.Attendee;
    var activity = notification.Activity;

    _logger.LogInformation("Domain Event: {DomainEvent}", notification.GetType().Name);

    var attendeeId = attendee.Identity.UserId;

    var attendeeInfo = await _userService.GetUserByIdAsync(attendeeId.Value, cancellationToken);

    GuardValidation.AgainstNull(attendeeInfo, $"can not found user {attendeeId}");

    var hostId = activity.Attendees
                         .FirstOrDefault(x => x.Identity.IsHost)!
                         .Identity.UserId;

    if (hostId == attendeeId) return;

    const string methodName = "ReceiveNotificationMessage";

    var message =
        $"New attendee {attendeeInfo!.DisplayName} joined your activity {activity.Title}.";

    var newNotification = await _mediator.Send(new CreateNewNotificationCommand
                                               {
                                                   Context = message,
                                                   RelatedId = attendeeId.Value,
                                                   NotificationType =
                                                       NotificationType.AttendeeAdded,
                                                   UserIds =
                                                       new List<UserId>
                                                       {
                                                           hostId
                                                       }
                                               },
                                               cancellationToken);

    var notificationDto = _mapper.Map<NotificationDto>(newNotification);

    await _notificationService.SendMessageToUser(methodName,
                                                 hostId.Value,
                                                 notificationDto);
  }
}
