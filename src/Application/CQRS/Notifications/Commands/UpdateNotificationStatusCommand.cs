using Application.Common.Helpers;
using Application.common.Interfaces;
using Application.Common.Interfaces;
using Application.common.Models;

using AutoMapper;

using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using Domain.ValueObjects.Message;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Notifications.Commands;

public record UpdateNotificationStatusCommand : IRequest<Result>
{
  public string NotificationId { get; init; }
  public string UserId { get; init; }
}

public class
    UpdateNotificationStatusCommandHandler : IRequestHandler<
    UpdateNotificationStatusCommand, Result>
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly INotificationRepository _notificationRepository;
  private readonly IMapper _mapper;
  private readonly ILogger<UpdateNotificationStatusCommandHandler> _logger;

  public UpdateNotificationStatusCommandHandler(
      IMapper                                         mapper,
      ILogger<UpdateNotificationStatusCommandHandler> logger,
      INotificationRepository                     notificationRepository,
      IUnitOfWork                                     unitOfWork)
  {
    _mapper = mapper;
    _logger = logger;
    _notificationRepository = notificationRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task<Result> Handle(
      UpdateNotificationStatusCommand request,
      CancellationToken               cancellationToken)
  {
    try
    {
      var notification = await _notificationRepository.GetNotificationWithUserByIdAsync(new NotificationId(request.NotificationId));

      GuardValidation.AgainstNull(notification, "notification not found");

      var userNotification = notification!.UserNotifications.FirstOrDefault(x => x.UserId == new UserId(request.UserId));

      GuardValidation.AgainstNull(userNotification, "notification not found");

      userNotification!.MarkAsRead();

      var result = await _unitOfWork.SaveChangesAsync(cancellationToken) != 0;

      if (!result)
      {
        throw new Exception("There was an error saving data to the database");
      }

      return Result.Success();
    }
    catch (Exception ex)
    {
      _logger.LogError(ex,
                       "Error occurred in {Name}: {ExMessage}",
                       nameof(UpdateNotificationStatusCommand),
                       ex.Message);

      throw;
    }
  }
}
