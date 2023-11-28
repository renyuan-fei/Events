using Application.Common.Interfaces;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Activities.Queries.GetActivity;

public record GetActivityByIdQuery : IRequest<Activity>
{
  public Guid Id { get; set; }
}

public class
    GetActivityByIdQueryHandler : IRequestHandler<GetActivityByIdQuery, Activity>
{
  private readonly IApplicationDbContext _context;

  public GetActivityByIdQueryHandler(IApplicationDbContext context)
  {
    _context = context;
  }

  public async Task<Activity> Handle(
      GetActivityByIdQuery request,
      CancellationToken      cancellationToken)
  {
    return (await _context.Activities
                          .FirstOrDefaultAsync(x => x.Id == request.Id,
                                               cancellationToken));
  }
}
