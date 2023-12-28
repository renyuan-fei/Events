using System.Reflection;

using Application.common.Interfaces;

using MediatR;

namespace Application.common.Behaviours;

public class
    AuthorizationBehaviour <TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
where TRequest : notnull
{
  private readonly ICurrentUserService            _user;
  private readonly IIdentityService _identityService;

  public AuthorizationBehaviour(
      ICurrentUserService           user,
      IIdentityService identityService)
  {
    _user = user;
    _identityService = identityService;
  }

  public async Task<TResponse> Handle(
      TRequest                          request,
      RequestHandlerDelegate<TResponse> next,
      CancellationToken                 cancellationToken)
  {
    var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

    var attributes = authorizeAttributes.ToList();

    if (!attributes.Any()) return await next();

    // Must be authenticated user
    if (_user.Id == null) { throw new UnauthorizedAccessException(); }

    // Role-based authorization
    var authorizeAttributesWithRoles =
        attributes.Where(a => !string.IsNullOrWhiteSpace(a.Roles));

    var attributesWithRoles = authorizeAttributesWithRoles.ToList();

    if (attributesWithRoles.Any())
    {
      var authorized = false;

      foreach (var roles in
               attributesWithRoles.Select(a => a.Roles.Split(',')))
      {
        foreach (var role in roles)
        {
          var isInRole = await _identityService.IsInRoleAsync(_user.Id, role.Trim());

          if (isInRole)
          {
            authorized = true;

            break;
          }
        }
      }

      // Must be a member of at least one role in roles
      if (!authorized) { throw new ForbiddenAccessException(); }
    }

    // Policy-based authorization
    var authorizeAttributesWithPolicies =
        attributes.Where(a => !string.IsNullOrWhiteSpace(a.Policy));

    var attributesWithPolicies = authorizeAttributesWithPolicies as AuthorizeAttribute[ ] ?? authorizeAttributesWithPolicies.ToArray();

    if (!attributesWithPolicies.Any()) return await next();

    {
      foreach (var policy in attributesWithPolicies.Select(a => a.Policy))
      {
        var authorized = await _identityService.AuthorizeAsync(_user.Id, policy);

        if (!authorized) { throw new ForbiddenAccessException(); }
      }
    }

    // User is authorized / authorization not required
    return await next();
  }
}
