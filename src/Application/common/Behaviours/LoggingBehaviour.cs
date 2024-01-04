using Application.common.Interfaces;

using MediatR.Pipeline;

using Microsoft.Extensions.Logging;

namespace Application.common.Behaviours;

public class LoggingBehaviour <TRequest> : IRequestPreProcessor<TRequest>
where TRequest : notnull
{
  private readonly IIdentityService    _identityService;
  private readonly ILogger             _logger;
  private readonly ICurrentUserService _user;

  public LoggingBehaviour(
      ILogger<TRequest>   logger,
      ICurrentUserService user,
      IIdentityService    identityService)
  {
    _logger = logger;
    _user = user;
    _identityService = identityService;
  }

  public async Task Process(TRequest request, CancellationToken cancellationToken)
  {
    var requestName = typeof(TRequest).Name;
    var userId = _user.Id ?? string.Empty;
    var userName = string.Empty;

    if (!string.IsNullOrEmpty(userId))
    {
      userName = await _identityService.GetUserNameAsync(userId);
    }

    _logger.LogInformation("tmp Request: {Name} {@UserId} {@UserName} {@Request}",
                           requestName,
                           userId,
                           userName,
                           request);
  }
}
