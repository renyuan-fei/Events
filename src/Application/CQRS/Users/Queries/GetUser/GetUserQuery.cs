using Application.common.DTO;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Users.Queries.GetUser;

// Used for bypassing authorization behavior checks.
[ BypassAuthorization ]
public record GetUserQuery : IRequest<UserProfileDTO>
{
  public Guid UserId { get; init; }
}

public class GetUserHandler : IRequestHandler<GetUserQuery, UserProfileDTO>
{
  private readonly ILogger<GetUserHandler> _logger;
  private readonly IMapper                 _mapper;

  public GetUserHandler(
      IMapper                 mapper,
      ILogger<GetUserHandler> logger)
  {
    _mapper = mapper;
    _logger = logger;
  }

  public async Task<UserProfileDTO> Handle(
      GetUserQuery      request,
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
