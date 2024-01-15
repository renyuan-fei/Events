using System.Security.Claims;

using Application.common.DTO;

namespace Application.common.interfaces;

/// <summary>
///   Represents a service for generating and validating JWT tokens.
/// </summary>
public interface IJwtTokenService
{
  /// <summary>
  ///   Creates an authentication token based on the provided token information.
  /// </summary>
  /// <param name="tokenDTO">The TokenDTO object containing the token information.</param>
  /// <param name="isRefresh">
  ///   A boolean flag indicating whether the token is being refreshed.
  ///   (Optional, default value is false)
  /// </param>
  /// <returns>The AuthenticationDTO object representing the created authentication token.</returns>
  AuthenticationDTO CreateToken(TokenDTO tokenDTO, bool isRefresh = false);

  /// <summary>
  ///   Retrieves a <see cref="ClaimsPrincipal" /> object from a JWT token.
  /// </summary>
  /// <param name="token">The JWT token.</param>
  /// <param name="validateLifetime"></param>
  /// <returns>
  ///   A <see cref="ClaimsPrincipal" /> object if the token is valid and contains valid
  ///   claims,
  ///   or null if the token is invalid or does not contain any claims.
  /// </returns>
  ClaimsPrincipal? GetPrincipalFromJwtToken(string? token, bool validateLifetime = true);
}
