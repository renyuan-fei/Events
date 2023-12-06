using Application.Common.Interfaces;

using AutoMapper;

using Domain.Entities;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.ActivityAttendee.Commands.CreateActivityAttendee;

public record CreateActivityAttendeeCommand : IRequest<Unit>
{
  //TODO
}

public class CreateActivityAttendeeHandler : IRequestHandler<CreateActivityAttendeeCommand, Unit>
{
  private readonly IApplicationDbContext                  _context;
  private readonly IMapper                                _mapper;
  private readonly ILogger<CreateActivityAttendeeHandler> _logger;

  public CreateActivityAttendeeHandler(
      IApplicationDbContext                  context,
      IMapper                                mapper,
      ILogger<CreateActivityAttendeeHandler> logger)
  {
    _context = context;
    _mapper = mapper;
    _logger = logger;
  }

  public async Task<Unit> Handle(
      CreateActivityAttendeeCommand request,
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

