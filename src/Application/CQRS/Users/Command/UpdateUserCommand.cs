using Application.common.DTO;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Users.Command;

public record UpdateUserCommand : IRequest<Unit>
{
  public Guid    UserId { get; init; }
  public UserDTO user   { get; init; }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
{
  private readonly ILogger<UpdateUserCommandHandler> _logger;
  private readonly IMapper                           _mapper;

  public UpdateUserCommandHandler(
      IMapper                           mapper,
      ILogger<UpdateUserCommandHandler> logger
      )
  {
    _mapper = mapper;
    _logger = logger;
  }

  public async Task<Unit> Handle(
      UpdateUserCommand request,
      CancellationToken cancellationToken)
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
