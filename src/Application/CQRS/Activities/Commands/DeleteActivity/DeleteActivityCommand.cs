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
  private readonly IEventsDbContext                      _context;
  private readonly ILogger<DeleteActivityCommandHandler> _logger;
  private readonly IMapper                               _mapper;

  public DeleteActivityCommandHandler(
      IMapper                               mapper,
      ILogger<DeleteActivityCommandHandler> logger,
      IEventsDbContext                      context)
  {
    _mapper = mapper;
    _logger = logger;
    _context = context;
  }

  public async Task<Unit> Handle(
      DeleteActivityCommand request,
      CancellationToken     cancellationToken)
  {
    try
    {
      throw new NotImplementedException();
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "ErrorMessage saving to the database: {ExMessage}", ex.Message);

      throw;
    }
  }
}
