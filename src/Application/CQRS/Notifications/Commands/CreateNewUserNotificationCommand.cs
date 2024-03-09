using Application.Common.Interfaces;

using AutoMapper;

using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Notifications.Commands;

public record CreateNewUserNotificationCommand : IRequest<UserNotification>
{
  public string           context          { get; init; }
  public UserId           userId           { get; init; }
  public NotificationType notificationType { get; init; }
}

public class
    CreateNewUserNotificationCommandHandler :
    IRequestHandler<CreateNewUserNotificationCommand, UserNotification>
{
  private readonly IMapper                                          _mapper;
  private readonly ILogger<CreateNewUserNotificationCommandHandler> _logger;

  public CreateNewUserNotificationCommandHandler(
      IMapper                                          mapper,
      ILogger<CreateNewUserNotificationCommandHandler> logger)
  {
    _mapper = mapper;
    _logger = logger;
  }

  public async Task<UserNotification> Handle(
      CreateNewUserNotificationCommand request,
      CancellationToken                cancellationToken)
  {
    try { throw new NotImplementedException(); }
    catch (Exception ex)
    {
      _logger.LogError(ex,
                       "Error occurred in {Name}: {ExMessage}",
                       nameof(CreateNewUserNotificationCommand),
                       ex.Message);

      throw;
    }
  }
}

