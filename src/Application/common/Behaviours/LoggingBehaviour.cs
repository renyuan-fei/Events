using MediatR.Pipeline;

using Microsoft.Extensions.Logging;

namespace Application.common.Behaviours;

public class LoggingBehaviour <TRequest> : IRequestPreProcessor<TRequest>
where TRequest : notnull
{
  private readonly ILogger<TRequest> _logger;
  private readonly ICurrentUserService _currentUserService;
  private readonly IIdentityService  _identityService;

  public LoggingBehaviour(
      ILogger<TRequest>   logger,
      IIdentityService    identityService,
      ICurrentUserService currentUserService)
  {
    _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    _identityService = identityService;
    _currentUserService = currentUserService;
  }

  public async Task Process(TRequest request, CancellationToken cancellationToken)
  {
    var requestName = typeof(TRequest).Name;

    try
    {
      if (_currentUserService.UserId.HasValue)
      {
        var userId = _currentUserService.UserId.Value;
        var userName = await _identityService.GetUserNameAsync(userId);

        _logger.LogInformation("Request: {Name} {@UserId} {@UserName} {@Request}",
                               requestName,
                               userId,
                               userName,
                               request);
      }
      else { _logger.LogInformation("Request: {Name} {@Request}", requestName, request); }
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "An error occurred while logging request {Name}", requestName);
    }
  }
}
