using System.Security.Claims;

using Application.common.Interfaces;
using Application.Common.Interfaces;

using Domain.Repositories;
using Domain.ValueObjects;
using Domain.ValueObjects.Activity;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Application.common.Security;

public class IsHostRequirement : IAuthorizationRequirement
{
}

public class IsHostRequirementHandler : AuthorizationHandler<IsHostRequirement>
{
  private readonly IActivityRepository  _activityRepository;
  private readonly IHttpContextAccessor _httpContextAccessor;

  public IsHostRequirementHandler(
      IHttpContextAccessor httpContextAccessor,
      IActivityRepository  activityRepository)
  {
    _httpContextAccessor = httpContextAccessor;
    _activityRepository = activityRepository;
  }

  protected override Task HandleRequirementAsync(
      AuthorizationHandlerContext context,
      IsHostRequirement           requirement)
  {
    var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

    if (userId == null) return Task.CompletedTask;

    var activityId = _httpContextAccessor.HttpContext?.Request.RouteValues
                                         .SingleOrDefault(x => x.Key == "id")
                                         .Value?.ToString()!;

    var isHost = _activityRepository
                 .IsHostAsync(new ActivityId(activityId), new UserId(userId))
                 .GetAwaiter()
                 .GetResult();

    switch (isHost)
    {
      case false : return Task.CompletedTask;

      case true : context.Succeed(requirement);

        break;
    }

    return Task.CompletedTask;
  }
}
