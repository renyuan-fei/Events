using System.Security.Claims;

using Application.common.interfaces;

namespace WebAPI.Services;

/// The CurrentUserService class is responsible for retrieving the currently logged-in user's ID from the HttpContext.
/// It implements the ICurrentUserService interface.
/// /
public class CurrentUserService : ICurrentUserService
{
  private readonly IHttpContextAccessor _httpContextAccessor;

  public CurrentUserService(IHttpContextAccessor httpContextAccessor)
  {
    _httpContextAccessor = httpContextAccessor;
  }

  public string? Id =>
      _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
}
