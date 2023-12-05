using System.Diagnostics;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.common.Behaviours;

/// <summary>
///   Logging the request that is Overrun
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public class
    PerformanceBehaviour <TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
where TRequest : notnull
{
  private readonly ICurrentUserService _currentUser;
  private readonly IIdentityService    _identityService;
  private readonly ILogger<TRequest>   _logger;
  private readonly Stopwatch           _timer = new();

  public PerformanceBehaviour(
      ILogger<TRequest>   logger,
      IIdentityService    identityService,
      ICurrentUserService currentUser)
  {
    _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    _identityService = identityService;
    _currentUser = currentUser;
  }

  /// <summary>
  ///   Handle the request
  /// </summary>
  /// <param name="request"></param>
  /// <param name="next"></param>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  public async Task<TResponse> Handle(
      TRequest                          request,
      RequestHandlerDelegate<TResponse> next,
      CancellationToken                 cancellationToken)
  {
    _timer.Start();

    var response = await next();

    _timer.Stop();

    var elapsedMilliseconds = _timer.ElapsedMilliseconds;

    if (elapsedMilliseconds > 500)
    {
      var requestName = typeof(TRequest).Name;
      var userId = _currentUser.UserId ?? Guid.Empty;

      var userName = userId != Guid.Empty
          ? await _identityService.GetUserNameAsync(userId)
          : "Anonymous";

      _logger.LogWarning("Long Running Request: {Name} ({ElapsedMilliseconds} ms) {@UserId} {@UserName} {@Request}",
                         requestName,
                         elapsedMilliseconds,
                         userId,
                         userName,
                         request);
    }

    _timer.Reset(); // Reset the timer for the next use.

    return response;
  }
}
