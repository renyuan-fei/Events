using Application.Common.Interfaces;
using Application.common.Models;

using AutoMapper;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.Commands.DeleteActivity;

public record DeleteActivityCommand : IRequest<Result>
{
  public Guid Id { get; init; }
}

public class
    DeleteActivityCommandHandler : IRequestHandler<DeleteActivityCommand, Result>
{
  private readonly IApplicationDbContext                 _context;
  private readonly IMapper                               _mapper;
  private readonly ILogger<DeleteActivityCommandHandler> _logger;

  public DeleteActivityCommandHandler(
      IMapper                               mapper,
      IApplicationDbContext                 context,
      ILogger<DeleteActivityCommandHandler> logger)
  {
    _mapper = mapper;
    _context = context;
    _logger = logger;
  }

  public async Task<Result> Handle(
      DeleteActivityCommand request,
      CancellationToken     cancellationToken)
  {
    var entity =
        await _context.Activities.FindAsync(new object[ ] { request.Id },
                                            cancellationToken);

    if (entity == null) return null!;

    _context.Activities.Remove(entity);
    var result = await _context.SaveChangesAsync(cancellationToken) > 0;

    return result
        ? Result.Success()
        : Result.Failure(new[ ] { "Failed to delete activity" });
  }
}
