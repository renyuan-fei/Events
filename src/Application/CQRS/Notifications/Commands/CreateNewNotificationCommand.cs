using Application.common.Interfaces;
using Application.Common.Interfaces;
using Application.common.Models;

using AutoMapper;

using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using Domain.ValueObjects;
using Domain.ValueObjects.Activity;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Notifications.Commands;

public record CreateNewNotificationCommand : IRequest<Notification>
{
  public string           Context          { get; init; }
  public string           RelatedId        { get; init; }
  public NotificationType NotificationType { get; init; }
  public List<UserId>     UserIds          { get; init; }
}

public class
    CreateNewNotificationCommandHandler : IRequestHandler<CreateNewNotificationCommand,
    Notification>
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly INotificationRepository _notificationRepository;
  private readonly IUserNotificationRepository _userNotificationRepository;
  private readonly IMapper _mapper;
  private readonly ILogger<CreateNewNotificationCommandHandler> _logger;

  public CreateNewNotificationCommandHandler(
      IMapper                                      mapper,
      ILogger<CreateNewNotificationCommandHandler> logger,
      IUserNotificationRepository                  userNotificationRepository,
      INotificationRepository                      notificationRepository,
      IUnitOfWork                                  unitOfWork)
  {
    _mapper = mapper;
    _logger = logger;
    _userNotificationRepository = userNotificationRepository;
    _notificationRepository = notificationRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task<Notification> Handle(
      CreateNewNotificationCommand request,
      CancellationToken            cancellationToken)
  {
    try
    {
      var context = request.Context;
      var relatedId = request.RelatedId;
      var notificationType = request.NotificationType;
      var userIds = request.UserIds;

      var notification =
          Notification.Create(context,
                              relatedId,
                              notificationType);

      // use add UserNotification add all of them to the notification
      notification.AddUserNotifications(userIds);

      await _notificationRepository.AddAsync(notification, cancellationToken);

      return notification;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex,
                       "Error occurred in {Name}: {ExMessage}",
                       nameof(CreateNewNotificationCommand),
                       ex.Message);

      throw;
    }
  }
}
