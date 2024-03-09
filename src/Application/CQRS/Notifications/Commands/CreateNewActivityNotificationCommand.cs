using Application.Common.Interfaces;

using AutoMapper;

using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using Domain.ValueObjects.Activity;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Notifications.Commands;

public record CreateNewActivityNotificationCommand : IRequest<Notification>
{
  public string context { get; init; }
  public ActivityId activityId { get; init; }
  public NotificationType notificationType { get; init; }
}

public class
    CreateNewNotificationCommandHandler : IRequestHandler<CreateNewActivityNotificationCommand,
    Notification>
{
  private readonly IUserNotificationRepository _userNotificationRepository;
  private readonly INotificationRepository _notificationRepository;
  private readonly IMapper _mapper;
  private readonly ILogger<CreateNewNotificationCommandHandler> _logger;

  public CreateNewNotificationCommandHandler(
      IMapper                                      mapper,
      ILogger<CreateNewNotificationCommandHandler> logger,
      IUserNotificationRepository                  userNotificationRepository,
      INotificationRepository                      notificationRepository)
  {
    _mapper = mapper;
    _logger = logger;
    _userNotificationRepository = userNotificationRepository;
    _notificationRepository = notificationRepository;
  }

  public async Task<Notification> Handle(
      CreateNewActivityNotificationCommand request,
      CancellationToken            cancellationToken)
  {
    try { throw new NotImplementedException(); }
    catch (Exception ex)
    {
      _logger.LogError(ex,
                       "Error occurred in {Name}: {ExMessage}",
                       nameof(CreateNewActivityNotificationCommand),
                       ex.Message);

      throw;
    }
  }
}
