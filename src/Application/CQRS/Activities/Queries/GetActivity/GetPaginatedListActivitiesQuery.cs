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
  private readonly IUserService                                    _userService;

  public GetPaginatedListActivitiesQueryHandler(
      IMapper                                         mapper,
      ILogger<GetPaginatedListActivitiesQueryHandler> logger,
      IEventsDbContext                                context,
      IUserService                                    userService)
  {
    _mapper = mapper;
    _logger = logger;
    _context = context;
    _userService = userService;
  }

  public async Task<PaginatedList<ActivityDTO>> Handle(
      GetPaginatedListActivitiesQuery request,
      CancellationToken               cancellationToken)
  {
    throw new NotImplementedException();
  }
}
