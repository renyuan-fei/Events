using Application.common.DTO;
using Application.Common.Interfaces;
using Application.common.Mappings;
using Application.common.Models;

using AutoMapper;

using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Notifications.Queries;

public record GetNotificationQuery : IRequest<List<NotificationDto>>
{
  public string UserId { get; init; }
  public PaginatedListParams PaginatedListParams { get; init; } = new();
}

public class GetNotificationQueryHandler : IRequestHandler<GetNotificationQuery, List<NotificationDto>>
{
  private readonly INotificationRepository _notificationRepository;
  private readonly IMapper                              _mapper;
  private readonly ILogger<GetNotificationQueryHandler> _logger;

  public GetNotificationQueryHandler(
      IMapper                              mapper,
      ILogger<GetNotificationQueryHandler> logger,
      INotificationRepository              notificationRepository)
  {
    _mapper = mapper;
    _logger = logger;
    _notificationRepository = notificationRepository;
  }

  public async Task<List<NotificationDto>> Handle(
      GetNotificationQuery request,
      CancellationToken    cancellationToken)
  {
    try
    {
      var userId = new UserId(request.UserId);

      var notificationsQuery =
          _notificationRepository.GetUserNotificationQueryable(userId);

      var orderedQueryable =
          notificationsQuery.OrderBy(notification => notification.Created);

      var notifications =
          await orderedQueryable.ToListAsync(cancellationToken: cancellationToken);

      var notificationDtos = _mapper.Map<List<NotificationDto>>(notifications);

      return notificationDtos;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex,
                       "Error occurred in {Name}: {ExMessage}",
                       nameof(GetNotificationQuery),
                       ex.Message);

      throw;
    }
  }
}

