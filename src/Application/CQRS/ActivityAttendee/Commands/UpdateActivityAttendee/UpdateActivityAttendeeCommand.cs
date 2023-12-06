using Application.Common.Interfaces;

using AutoMapper;

using Domain.Entities;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.ActivityAttendee.Commands.UpdateActivityAttendee;

public record UpdateActivityAttendeeCommand : IRequest<Unit>
{
  //TODO
}

public class UpdateActivityAttendeeHandler : IRequestHandler<UpdateActivityAttendeeCommand, Unit>
{
  private readonly IApplicationDbContext                  _context;
  private readonly IMapper                                _mapper;
  private readonly ILogger<UpdateActivityAttendeeHandler> _logger;

  public UpdateActivityAttendeeHandler(
      IApplicationDbContext                  context,
      IMapper                                mapper,
      ILogger<UpdateActivityAttendeeHandler> logger)
  {
    _context = context;
    _mapper = mapper;
    _logger = logger;
  }

  public async Task<Unit> Handle(
      UpdateActivityAttendeeCommand request,
      CancellationToken      cancellationToken)
  {
    try
    {
      throw new NotImplementedException();

    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error saving to the database: {ExMessage}", ex.Message);

      throw;
    }
  }
}

