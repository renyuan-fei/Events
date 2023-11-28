using Application.Common.Interfaces;

using Domain.Entities;

using MediatR;

namespace Application.CQRS.Activities.Commands.UpdateActivity;

public record UpdateActivity : IRequest
{
  public Guid     Id       { get; init; }
  public Activity Activity { get; init; }
}

public class UpdateActivityHandler : IRequestHandler<UpdateActivity>
{
  private readonly IApplicationDbContext _context;

  public UpdateActivityHandler(IApplicationDbContext context) { _context = context; }

  public async Task Handle(
      UpdateActivity    request,
      CancellationToken cancellationToken)
  {
    var entity = await _context.Activities
                               .FindAsync(new object[ ] { request.Id },
                                          cancellationToken);

    if (entity == null) return;

    entity.Title = request.Activity.Title;
    entity.Description = request.Activity.Description;
    entity.Date = request.Activity.Date;
    entity.Category = request.Activity.Category;
    entity.City = request.Activity.City;
    entity.Venue = request.Activity.Venue;

    await _context.SaveChangesAsync(cancellationToken);
  }
}
