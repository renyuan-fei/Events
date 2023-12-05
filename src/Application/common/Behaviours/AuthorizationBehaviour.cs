using System.Reflection;

using MediatR;

namespace Application.common.Behaviours;

/// <summary>
///   Authorization for Role and Policy
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public class
    AuthorizationBehaviour <TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
where TRequest : notnull
{
  private readonly ICurrentUserService _currentUser;
  private readonly IIdentityService    _identityService;

  public AuthorizationBehaviour(
      ICurrentUserService currentUserService,
      IIdentityService    identityService,
      ICurrentUserService currentUser)
  {
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
  /// <exception cref="UnauthorizedAccessException"></exception>
  /// <exception cref="ForbiddenAccessException"></exception>
  public async Task<TResponse> Handle(
      TRequest                          request,
      RequestHandlerDelegate<TResponse> next,
      CancellationToken                 cancellationToken)
  {
    if (_currentUser.UserId == null) { throw new UnauthorizedAccessException(); }

    var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

    foreach (var attribute in authorizeAttributes)
    {
      // Role-based authorization
      if (!string.IsNullOrWhiteSpace(attribute.Roles))
      {
        var roles = attribute.Roles.Split(',').Select(r => r.Trim());
        var authorizedInAnyRole = false;

        foreach (var role in roles)
        {
          if (!await _identityService.IsInRoleAsync(_currentUser.UserId.Value, role))
            continue;

          authorizedInAnyRole = true;

          break;
        }

        if (!authorizedInAnyRole) { throw new ForbiddenAccessException(); }
      }

      // Policy-based authorization
      if (!string.IsNullOrWhiteSpace(attribute.Policy)
       && !await _identityService.AuthorizeAsync(_currentUser.UserId.Value,
                                                 attribute.Policy))
      {
        throw new ForbiddenAccessException();
      }
    }

    return await next();
  }
}
