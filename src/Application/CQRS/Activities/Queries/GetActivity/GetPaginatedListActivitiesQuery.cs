using Application.common.DTO;
using Application.common.Interfaces;
using Application.Common.Interfaces;
using Application.common.Mappings;
using Application.common.Models;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.Queries.GetActivity;

[ BypassAuthorization ]
public record GetPaginatedListActivitiesQuery : IRequest<PaginatedList<ActivityDTO>>
{
  public PaginatedListParams PaginatedListParams { get; init; }
  public FilterParams?       FilterParams        { get; init; }
}

public class
    GetPaginatedListActivitiesQueryHandler :
    IRequestHandler<GetPaginatedListActivitiesQuery,
    PaginatedList<ActivityDTO>>
{
  private readonly IEventsDbContext                                _context;
  private readonly ILogger<GetPaginatedListActivitiesQueryHandler> _logger;
  private readonly IMapper                                         _mapper;

  public GetPaginatedListActivitiesQueryHandler(
      IMapper                                         mapper,
      ILogger<GetPaginatedListActivitiesQueryHandler> logger,
      IEventsDbContext                                context)
  {
    _mapper = mapper;
    _logger = logger;
    _context = context;
  }

  public async Task<PaginatedList<ActivityDTO>> Handle(
      GetPaginatedListActivitiesQuery request,
      CancellationToken               cancellationToken)
  {
    throw new NotImplementedException();
  }
}
