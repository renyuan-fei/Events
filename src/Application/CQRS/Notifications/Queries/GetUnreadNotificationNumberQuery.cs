using Application.Common.Interfaces;

using AutoMapper;

using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Notifications.Queries;

public record GetUnreadNotificationNumberQuery : IRequest<int>
{
  public string UserId { get; init; }
}

public class
    GetUnreadNotificationNumberQueryHandler : IRequestHandler<
    GetUnreadNotificationNumberQuery, int>
{
  private readonly IUserNotificationRepository _userNotificationRepository;
  private readonly IMapper                                          _mapper;
  private readonly ILogger<GetUnreadNotificationNumberQueryHandler> _logger;

  public GetUnreadNotificationNumberQueryHandler(

      IMapper                                          mapper,
      ILogger<GetUnreadNotificationNumberQueryHandler> logger,
      IUserNotificationRepository userNotificationRepository)
  {
    _mapper = mapper;
    _logger = logger;
    _userNotificationRepository = userNotificationRepository;
  }

  public async Task<int> Handle(
      GetUnreadNotificationNumberQuery request,
      CancellationToken                cancellationToken)
  {
    try
    {
      var userId = new UserId(request.UserId);

      var notificationCount = await _userNotificationRepository.GetNotificationCountByUserIdAsync(userId);

      return notificationCount;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex,
                       "Error occurred in {Name}: {ExMessage}",
                       nameof(GetUnreadNotificationNumberQuery),
                       ex.Message);

      throw;
    }
  }
}

