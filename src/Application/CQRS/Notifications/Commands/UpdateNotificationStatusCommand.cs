using Application.Common.Helpers;
using Application.common.Interfaces;
using Application.Common.Interfaces;
using Application.common.Models;

using AutoMapper;

using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects.Message;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Notifications.Commands;

public record UpdateNotificationStatusCommand : IRequest<Result>
{
  public string UserNotificationId { get; init; }
}

public class
    UpdateNotificationStatusCommandHandler : IRequestHandler<
    UpdateNotificationStatusCommand, Result>
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IUserNotificationRepository _userNotificationRepository;
  private readonly IMapper _mapper;
  private readonly ILogger<UpdateNotificationStatusCommandHandler> _logger;

  public UpdateNotificationStatusCommandHandler(
      IMapper                                         mapper,
      ILogger<UpdateNotificationStatusCommandHandler> logger,
      IUserNotificationRepository                     userNotificationRepository,
      IUnitOfWork                                     unitOfWork)
  {
    _mapper = mapper;
    _logger = logger;
    _userNotificationRepository = userNotificationRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task<Result> Handle(
      UpdateNotificationStatusCommand request,
      CancellationToken               cancellationToken)
  {
    try
    {
      var userNotification = await _userNotificationRepository.GetByIdAsync(new
          UserNotificationId(request.UserNotificationId));

      GuardValidation.AgainstNull(userNotification, "notification not found");

      userNotification.MarkAsRead();

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
