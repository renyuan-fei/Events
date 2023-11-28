using Application.Common.Interfaces;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Activities.Queries.GetActivity;

public record GetAllActivitiesQuery : IRequest<List<Activity>>;

public class
    GetAllActivitiesQueryHandler : IRequestHandler<GetAllActivitiesQuery, List<Activity>>
{
  private readonly IApplicationDbContext _context;

  public GetAllActivitiesQueryHandler(IApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<List<Activity>> Handle(
      GetAllActivitiesQuery request,
      CancellationToken  cancellationToken)
  {
    return await _context.Activities.ToListAsync(cancellationToken);
  }
}
