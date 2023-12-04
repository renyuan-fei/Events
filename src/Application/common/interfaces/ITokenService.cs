using System.Security.Claims;

using Application.common.DTO;

namespace Application.common.interfaces;

public interface IJwtTokenService
{
  AuthenticationResponse CreateToken(IApplicationUser user);

  ClaimsPrincipal? GetPrincipalFromJwtToken(string? token);
}
