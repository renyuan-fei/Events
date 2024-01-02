using Application.Common.Interfaces;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Attendees.Commands.CreateAttendee;

public record CreateAttendeeCommand : IRequest<Unit>
{
  //TODO
}

public class
    CreateActivityAttendeeHandler : IRequestHandler<CreateAttendeeCommand, Unit>
{
  private readonly IEventsDbContext                       _context;
  private readonly ILogger<CreateActivityAttendeeHandler> _logger;
  private readonly IMapper                                _mapper;

  public CreateActivityAttendeeHandler(
      IMapper                                mapper,
      ILogger<CreateActivityAttendeeHandler> logger,
      IEventsDbContext                       context)
  {
    _mapper = mapper;
    _logger = logger;
    _context = context;
  }

  public async Task<Unit> Handle(
      CreateAttendeeCommand request,
      CancellationToken             cancellationToken)
  {
    try { throw new NotImplementedException(); }
    catch (Exception ex)
    {
      _logger.LogError(ex, "ErrorMessage saving to the database: {ExMessage}", ex.Message);

      throw;
    }
  }
}
