using Application.Common.Interfaces;

using AutoMapper;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.ActivityAttendee.Commands.DeleteActivityAttendee;

public record DeleteActivityAttendeeCommand : IRequest<Unit>
{
  //TODO
}

public class
    DeleteActivityAttendeeHandler : IRequestHandler<DeleteActivityAttendeeCommand, Unit>
{
  private readonly IEventsDbContext                       _context;
  private readonly ILogger<DeleteActivityAttendeeHandler> _logger;
  private readonly IMapper                                _mapper;

  public DeleteActivityAttendeeHandler(
      IMapper                                mapper,
      IEventsDbContext                       context,
      ILogger<DeleteActivityAttendeeHandler> logger)
  {
    _mapper = mapper;
    _context = context;
    _logger = logger;
  }

  public async Task<Unit> Handle(
      DeleteActivityAttendeeCommand request,
      CancellationToken             cancellationToken)
  {
    try { throw new NotImplementedException(); }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error saving to the database: {ExMessage}", ex.Message);

      throw;
    }
  }
}
