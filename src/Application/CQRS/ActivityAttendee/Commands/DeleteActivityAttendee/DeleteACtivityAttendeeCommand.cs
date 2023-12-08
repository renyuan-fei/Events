using Application.Common.Interfaces;

using AutoMapper;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.ActivityAttendee.Commands.DeleteActivityAttendee;

public record DeleteACtivityAttendeeCommand : IRequest<Unit>
{
  //TODO
}

public class
    DeleteACtivityAttendeeHandler : IRequestHandler<DeleteACtivityAttendeeCommand, Unit>
{
  private readonly IApplicationDbContext                  _context;
  private readonly ILogger<DeleteACtivityAttendeeHandler> _logger;
  private readonly IMapper                                _mapper;

  public DeleteACtivityAttendeeHandler(
      IApplicationDbContext                  context,
      IMapper                                mapper,
      ILogger<DeleteACtivityAttendeeHandler> logger)
  {
    _context = context;
    _mapper = mapper;
    _logger = logger;
  }

  public async Task<Unit> Handle(
      DeleteACtivityAttendeeCommand request,
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
