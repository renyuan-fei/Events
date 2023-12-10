using System.Security.Claims;

using Application.Common.Interfaces;

using Duende.IdentityServer;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.common.Security;

public class IsHostRequirement : IAuthorizationRequirement
{

}

public class IsHostRequirementHandler : AuthorizationHandler<IsHostRequirement>
{
  private readonly IApplicationDbContext _context;
  private readonly IHttpContextAccessor _httpContextAccessor;
  public IsHostRequirementHandler(IApplicationDbContext context , IHttpContextAccessor httpContextAccessor)
  {
    _context = context;
    _httpContextAccessor = httpContextAccessor;
  }

  protected override Task HandleRequirementAsync(
      AuthorizationHandlerContext context,
      IsHostRequirement           requirement)
  {
    var userId = Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    var activityId = Guid.Parse(_httpContextAccessor.HttpContext?.Request.RouteValues
                                                    .SingleOrDefault(x => x.Key == "id").Value?.ToString()!);

    var attendee = _context.ActivityAttendees
                             .AsNoTracking()
                             .FirstOrDefaultAsync(x => x.UserId == userId && x.Activity.Id ==
        activityId)
                             .Result;

    if (attendee == null) return Task.CompletedTask;

    if (attendee.IsHost) context.Succeed(requirement);

    return Task.CompletedTask;
  }
}
