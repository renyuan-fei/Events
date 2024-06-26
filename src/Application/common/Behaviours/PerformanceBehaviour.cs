﻿using System.Diagnostics;

using Application.common.Interfaces;

using Microsoft.Extensions.Logging;

namespace Application.common.Behaviours;

public class
    PerformanceBehaviour <TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
where TRequest : notnull
{
  private readonly IIdentityService    _identityService;
  private readonly ILogger<TRequest>   _logger;
  private readonly Stopwatch           _timer;
  private readonly ICurrentUserService _user;

  public PerformanceBehaviour(
      ILogger<TRequest>   logger,
      ICurrentUserService user,
      IIdentityService    identityService)
  {
    _timer = new Stopwatch();

    _logger = logger;
    _user = user;
    _identityService = identityService;
  }

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
      var userId = _user.Id ?? string.Empty;
      var userName = string.Empty;

      if (!string.IsNullOrEmpty(userId))
      {
        userName = await _identityService.GetUserNameAsync(userId);
      }

      _logger.LogWarning("tmp Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName} {@Request}",
                         requestName,
                         elapsedMilliseconds,
                         userId,
                         userName,
                         request);
    }

    return response;
  }
}
