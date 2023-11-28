using Application.Common.Interfaces;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.CQRS.Activities.Commands.CreateActivity;

public record CreateActivity : IRequest
{
  public Activity Activity { get; init; }
}

public class CreateActivityHandler : IRequestHandler<CreateActivity>
{
  private readonly IApplicationDbContext _context;

  public CreateActivityHandler(IApplicationDbContext context) { _context = context; }

  public async Task Handle(
      CreateActivity    request,
      CancellationToken cancellationToken)
  {
    _context.Activities.Add(request.Activity);

    await _context.SaveChangesAsync(cancellationToken);
  }
}
