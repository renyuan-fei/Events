using System.Security.Claims;

using Application.common.interfaces;

namespace WebAPI.Services;

/// The CurrentUserService class is responsible for retrieving the currently logged-in user's ID from the HttpContext.
/// It implements the ICurrentUserService interface.
/// /
public class CurrentUserService : ICurrentUserService
{
    /// <summary>
    ///   The _httpContextAccessor variable is an instance of the IHttpContextAccessor interface,
    ///   which is used to provide access to the current HttpContext.
    /// </summary>
    /// <remarks>
    ///   The IHttpContextAccessor interface is used in ASP.NET Core to encapsulate the context
    ///   of an individual HTTP request.
    ///   It provides access to the current HttpContext, which contains information about the
    ///   current request and response,
    ///   as well as access to various properties, headers, session, cookies, and more.
    /// </remarks>
    /// <seealso cref="IHttpContextAccessor" />
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    ///   Creates an instance of the CurrentUserService class.
    /// </summary>
    /// <param name="httpContextAccessor">
    ///   The IHttpContextAccessor instance which provides access
    ///   to the HttpContext.
    /// </param>
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
  {
    _httpContextAccessor = httpContextAccessor;
  }

    /// <summary>
    ///   Gets the user ID of the current user.
    /// </summary>
    /// <returns>The user ID as a nullable GUID.</returns>
    public Guid? UserId
  {
    get
    {
      var userIdValue =
          _httpContextAccessor.HttpContext?.User
                              ?.FindFirstValue(ClaimTypes.NameIdentifier);

      return TryParseGuid(userIdValue!);
    }
  }

    /// <summary>
    ///   Tries to parse a string representation of a GUID to a <see cref="Guid" /> object.
    /// </summary>
    /// <param name="value">The string representation of the GUID.</param>
    /// <returns>
    ///   A <see cref="Guid" /> object if the parsing is successful;
    ///   otherwise, <see langword="null" />.
    /// </returns>
    private static Guid? TryParseGuid(string value)
  {
    if (Guid.TryParse(value, out var result)) { return result; }

    return null;
  }
}
