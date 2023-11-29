using Application.Common.Interfaces;

using AutoMapper;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.Queries.GetActivity;

public record GetAllActivitiesQuery : IRequest<List<Activity>>;

public class
    GetAllActivitiesQueryHandler : IRequestHandler<GetAllActivitiesQuery, List<Activity>>
{
  private readonly IApplicationDbContext                 _context;
  private readonly IMapper                               _mapper;
  private readonly ILogger<GetAllActivitiesQueryHandler> _logger;

  public GetAllActivitiesQueryHandler(IApplicationDbContext context, IMapper mapper, ILogger<GetAllActivitiesQueryHandler> logger)
  {
    _context = context;
    _mapper = mapper;
    _logger = logger;
  }

  public async Task<List<Activity>> Handle(
      GetAllActivitiesQuery request,
      CancellationToken  cancellationToken)
  {
    return await _context.Activities.ToListAsync(cancellationToken);
  }
}
