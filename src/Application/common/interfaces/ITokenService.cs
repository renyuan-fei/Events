using System.Security.Claims;

using Application.common.DTO;

namespace Application.common.interfaces;

public interface IJwtTokenService
{
  AuthenticationDTO CreateToken(TokenDTO tokenDTO);

  ClaimsPrincipal? GetPrincipalFromJwtToken(string? token);
}
