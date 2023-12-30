using Application.common.DTO;
using Application.common.Interfaces;
using Application.Common.Interfaces;

using AutoMapper;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.Queries.GetActivity;

[ BypassAuthorization ]
public record GetAllActivitiesQuery : IRequest<List<ActivityDTO>>;

public class
    GetAllActivitiesQueryHandler : IRequestHandler<GetAllActivitiesQuery,
    List<ActivityDTO>>
{
  private readonly IEventsDbContext                      _context;
  private readonly ILogger<GetAllActivitiesQueryHandler> _logger;
  private readonly IMapper                               _mapper;

  public GetAllActivitiesQueryHandler(
      IMapper                               mapper,
      ILogger<GetAllActivitiesQueryHandler> logger,
      IMediator                             mediator,
      IEventsDbContext                      context)
  {
    _mapper = mapper;
    _logger = logger;
    _context = context;
  }

  public async Task<List<ActivityDTO>> Handle(
      GetAllActivitiesQuery request,
      CancellationToken     cancellationToken)
  {
    throw new NotImplementedException();
  }
}
