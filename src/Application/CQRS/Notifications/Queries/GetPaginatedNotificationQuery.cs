using Application.common.DTO;
using Application.Common.Interfaces;
using Application.common.Models;

using AutoMapper;

using Domain.Entities;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Notifications.Queries;

public record GetPaginatedNotificationQuery : IRequest<PaginatedList<NotificationDto>>
{
  public string UserId { get; init; }
}

public class
    GetPaginatedNotificationQueryHandler : IRequestHandler<GetPaginatedNotificationQuery
  , PaginatedList<NotificationDto>>
{
  private readonly IMapper                                        _mapper;
  private readonly ILogger<GetPaginatedNotificationQueryHandler> _logger;

  public GetPaginatedNotificationQueryHandler(
      IMapper                                        mapper,
      ILogger<GetPaginatedNotificationQueryHandler> logger)
  {
    _mapper = mapper;
    _logger = logger;
  }

  public async Task<PaginatedList<NotificationDto>> Handle(
      GetPaginatedNotificationQuery request,
      CancellationToken              cancellationToken)
  {
    try { throw new NotImplementedException(); }
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

