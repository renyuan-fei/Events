using Application.common.Interfaces;
using Application.Common.Interfaces;

using AutoMapper;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.Commands.CreateActivity;

/// <summary>
///   Represents the command for creating a new activity.
/// </summary>
public record CreateActivityCommand : IRequest<Unit>
{
  public Guid     CurrentUserId { get; init; }
  public Activity Activity      { get; init; }
}

public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand,
    Unit>
{
  private readonly IEventsDbContext                      _context;
  private readonly ILogger<CreateActivityCommandHandler> _logger;
  private readonly IMapper                               _mapper;

  public CreateActivityCommandHandler(
      IMapper                               mapper,
      ILogger<CreateActivityCommandHandler> logger,
      IEventsDbContext                      context)
  {
    _mapper = mapper;
    _logger = logger;
    _context = context;
  }

  public async Task<Unit> Handle(
      CreateActivityCommand request,
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
