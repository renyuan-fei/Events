using Application.Common.Interfaces;

using AutoMapper;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.Commands.DeleteActivity;

public record DeleteActivityCommand : IRequest<Activity>
{
  public Guid Id { get; init; }
}

public class
    DeleteActivityCommandHandler : IRequestHandler<DeleteActivityCommand, Activity>
{
  private readonly IApplicationDbContext                 _context;
  private readonly IMapper                               _mapper;
  private readonly ILogger<DeleteActivityCommandHandler> _logger;

  public DeleteActivityCommandHandler(IMapper mapper, IApplicationDbContext context, ILogger<DeleteActivityCommandHandler> logger)
  {
    _mapper = mapper;
    _context = context;
    _logger = logger;
  }

  public async Task<Activity> Handle(
      DeleteActivityCommand request,
      CancellationToken     cancellationToken)
  {
    var entity =
        await _context.Activities.FindAsync(new object[] { request.Id }, cancellationToken);

    if (entity == null) return null!;

    _context.Activities.Remove(entity);
    await _context.SaveChangesAsync(cancellationToken);

    return entity;
  }
}
