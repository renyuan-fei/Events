using System.Security.Claims;

using Application.Common.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.common.Security;

public class IsHostRequirement : IAuthorizationRequirement
{
}

public class IsHostRequirementHandler : AuthorizationHandler<IsHostRequirement>
{
  private readonly IEventsDbContext     _context;
  private readonly IHttpContextAccessor _httpContextAccessor;

  public IsHostRequirementHandler(
      IEventsDbContext     context,
      IHttpContextAccessor httpContextAccessor)
  {
    _context = context;
    _httpContextAccessor = httpContextAccessor;
  }

  protected override Task HandleRequirementAsync(
      AuthorizationHandlerContext context,
      IsHostRequirement           requirement)
  {
    throw new NotImplementedException();
  }
}
