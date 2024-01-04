using Application.Common.Interfaces;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Followers.Commands.UpdateFollowingCommand;

public record UpdateFollowerCommand : IRequest<Unit>
{
  public Guid UserId     { get; init; }
  public Guid FolloweeId { get; init; }
}

public class CreateFollowerCommandHandler : IRequestHandler<UpdateFollowerCommand, Unit>
{
  private readonly IEventsDbContext                      _context;
  private readonly ILogger<CreateFollowerCommandHandler> _logger;
  private readonly IMapper                               _mapper;

  public CreateFollowerCommandHandler(
      IEventsDbContext                      context,
      IMapper                               mapper,
      ILogger<CreateFollowerCommandHandler> logger)
  {
    _context = context;
    _mapper = mapper;
    _logger = logger;
  }

  public async Task<Unit> Handle(
      UpdateFollowerCommand request,
      CancellationToken     cancellationToken)
  {
    try { throw new NotImplementedException(); }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error saving to the database: {ExMessage}", ex.Message);

      throw;
    }
  }
}
