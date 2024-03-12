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

public record GetPaginatedNotificationQuery : IRequest<PaginatedList<NotificationDto>>
{
  public string UserId { get; init; }
  public PaginatedListParams PaginatedListParams { get; init; } = new();
}

public class
    GetPaginatedNotificationQueryHandler : IRequestHandler<GetPaginatedNotificationQuery
  , PaginatedList<NotificationDto>>
{
  private readonly INotificationRepository                       _notificationRepository;
  private readonly IMapper                                       _mapper;
  private readonly ILogger<GetPaginatedNotificationQueryHandler> _logger;

  public GetPaginatedNotificationQueryHandler(
      IMapper                                       mapper,
      ILogger<GetPaginatedNotificationQueryHandler> logger,
      INotificationRepository                       notificationRepository)
  {
    _mapper = mapper;
    _logger = logger;
    _notificationRepository = notificationRepository;
  }

  public async Task<PaginatedList<NotificationDto>> Handle(
      GetPaginatedNotificationQuery request,
      CancellationToken             cancellationToken)
  {
    try
    {
      var userId = new UserId(request.UserId);
      var initialTimestamp = request.PaginatedListParams.InitialTimestamp;
      var pageNumber = request.PaginatedListParams.PageNumber;
      var pageSize = request.PaginatedListParams.PageSize;

      var notificationsQuery =
          _notificationRepository.GetUserNotificationQueryable(userId,initialTimestamp);

      var orderedQueryable =
          notificationsQuery.OrderBy(notification => notification.Created);

      var paginatedList = await orderedQueryable
                                .ProjectTo<NotificationDto>(_mapper.ConfigurationProvider)
                                .PaginatedListAsync(pageNumber, pageSize);

      return paginatedList;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex,
                       "Error occurred in {Name}: {ExMessage}",
                       nameof(GetPaginatedNotificationQuery),
                       ex.Message);

      throw;
    }
  }
}
