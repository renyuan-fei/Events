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
    AttendeeRemovedDomainEventHandler : INotificationHandler<AttendeeRemovedDomainEvent>
{
  private readonly IMapper                                  _mapper;
  private readonly INotificationService                     _notificationService;
  private readonly IUserService                             _userService;
  private readonly IMediator                                _mediator;
  private readonly ILogger<AttendeeRemovedDomainEventHandler> _logger;

  public AttendeeRemovedDomainEventHandler(
      ILogger<AttendeeRemovedDomainEventHandler> logger,
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
      AttendeeRemovedDomainEvent notification,
      CancellationToken        cancellationToken)
  {
    // TODO when user leave an activity, let user leave the group of the activity
  }
}
