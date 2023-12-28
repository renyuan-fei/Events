using Application.Common.Interfaces;

using AutoMapper;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.Commands.UpdateActivity;

public record UpdateActivityCommand : IRequest<Unit>
{
  public Guid     Id       { get; init; }
  public Activity Activity { get; init; }
}

public class
    UpdateActivityCommandHandler : IRequestHandler<UpdateActivityCommand, Unit>
{
  private readonly IEventsDbContext                      _context;
  private readonly ILogger<UpdateActivityCommandHandler> _logger;
  private readonly IMapper                               _mapper;

  public UpdateActivityCommandHandler(
      IMapper                               mapper,
      ILogger<UpdateActivityCommandHandler> logger,
      IEventsDbContext                      context)
  {
    _mapper = mapper;
    _logger = logger;
    _context = context;
  }

  public async Task<Unit> Handle(
      UpdateActivityCommand request,
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
