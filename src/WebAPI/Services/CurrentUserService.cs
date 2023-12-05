using System.Security.Claims;

using Application.common.interfaces;

namespace WebAPI.Services;

/// <summary>
/// </summary>
public class CurrentUserService : ICurrentUserService
{
  private readonly IHttpContextAccessor _httpContextAccessor;

  /// <summary>
  /// </summary>
  /// <param name="httpContextAccessor"></param>
  public CurrentUserService(IHttpContextAccessor httpContextAccessor)
  {
    _httpContextAccessor = httpContextAccessor;
  }

  /// <summary>
  /// </summary>
  public Guid? UserId
  {
    get
    {
      var userIdValue =
          _httpContextAccessor.HttpContext?.User
                              ?.FindFirstValue(ClaimTypes.NameIdentifier);

      if (Guid.TryParse(userIdValue, out var userId)) { return userId; }

      return null;
    }
  }
}
