using Application.Common.Interfaces;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Attendees.Commands.UpdateAttendee;

public record UpdateAttendeeCommand : IRequest<Unit>
{
  public Guid Id         { get; init; }
  public Guid ActivityId { get; init; }
}

public class
    UpdateActivityAttendeeHandler : IRequestHandler<UpdateAttendeeCommand, Unit>
{
  private readonly IEventsDbContext                       _context;
  private readonly ILogger<UpdateActivityAttendeeHandler> _logger;
  private readonly IMapper                                _mapper;

  public UpdateActivityAttendeeHandler(
      IEventsDbContext                       context,
      IMapper                                mapper,
      ILogger<UpdateActivityAttendeeHandler> logger)
  {
    _context = context;
    _mapper = mapper;
    _logger = logger;
  }

  public async Task<Unit> Handle(
      UpdateAttendeeCommand request,
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
