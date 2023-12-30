using Application.common.DTO;
using Application.common.Interfaces;
using Application.Common.Interfaces;

using AutoMapper;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.Queries.GetActivity;

[BypassAuthorization]
public record GetActivityByIdQuery : IRequest<ActivityDTO>
{
  public Guid Id { get; init; }
}

public class
    GetActivityByIdQueryHandler : IRequestHandler<GetActivityByIdQuery, ActivityDTO>
{
  private readonly IEventsDbContext                     _context;
  private readonly ILogger<GetActivityByIdQueryHandler> _logger;
  private readonly IMapper                              _mapper;

  public GetActivityByIdQueryHandler(
      IMapper                              mapper,
      ILogger<GetActivityByIdQueryHandler> logger,
      IEventsDbContext                     context)
  {
    _mapper = mapper;
    _logger = logger;
    _context = context;
  }

  public async Task<ActivityDTO> Handle(
      GetActivityByIdQuery request,
      CancellationToken    cancellationToken)
  {
    try
    {
      throw new NotImplementedException();
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "ErrorMessage Mapping to DTO: {ExMessage}", ex.Message);

      throw;
    }
  }
}
