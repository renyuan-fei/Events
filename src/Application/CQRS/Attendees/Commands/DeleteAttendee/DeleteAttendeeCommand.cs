using Application.Common.Interfaces;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Attendees.Commands.DeleteAttendee;

public record DeleteAttendeeCommand : IRequest<Unit>
{
  //TODO
}

public class
    DeleteActivityAttendeeHandler : IRequestHandler<DeleteAttendeeCommand, Unit>
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
      DeleteAttendeeCommand request,
      CancellationToken     cancellationToken)
  {
    try { throw new NotImplementedException(); }
    catch (Exception ex)
    {
      _logger.LogError(ex,
                       "ErrorMessage saving to the database: {ExMessage}",
                       ex.Message);

      throw;
    }
  }
}
