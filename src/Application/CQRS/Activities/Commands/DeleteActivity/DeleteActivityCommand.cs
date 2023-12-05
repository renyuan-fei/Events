using Application.Common.Interfaces;

using AutoMapper;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.Commands.DeleteActivity;

public record DeleteActivityCommand : IRequest<Unit>
{
  public Guid Id { get; init; }
}

public class
    DeleteActivityCommandHandler : IRequestHandler<DeleteActivityCommand, Unit>
{
  private readonly IApplicationDbContext                 _context;
  private readonly ILogger<DeleteActivityCommandHandler> _logger;
  private readonly IMapper                               _mapper;

  public DeleteActivityCommandHandler(
      IMapper                               mapper,
      IApplicationDbContext                 context,
      ILogger<DeleteActivityCommandHandler> logger)
  {
    _mapper = mapper;
    _context = context;
    _logger = logger;
  }

  public async Task<Unit> Handle(
      DeleteActivityCommand request,
      CancellationToken     cancellationToken)
  {
    var entity =
        await _context.Activities.FindAsync(new object[ ] { request.Id },
                                            cancellationToken);

    if (entity == null)
    {
      _logger.LogError("Could not find activity with id {Id}", request.Id);

      throw new NotFoundException(nameof(Activity), request.Id);
    }

    _context.Activities.Remove(entity);

    try
    {
      var result = await _context.SaveChangesAsync(cancellationToken) > 0;

      return result
          ? Unit.Value
          : throw new DbUpdateException($"Error deleting activity with id {request.Id}");
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error saving to the database: {ExMessage}", ex.Message);

      throw;
    }
  }
}
