using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using Application.common.DTO;
using Application.common.interfaces;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Service;

/// <summary>
/// </summary>
public class JwtTokenService : IJwtTokenService
{
  private readonly IConfiguration _configuration;

  /// <summary>
  /// </summary>
  /// <param name="configuration"></param>
  public JwtTokenService(IConfiguration configuration) { _configuration = configuration; }

  /// <summary>
  /// </summary>
  /// <param name="tokenDTO"></param>
  /// <returns></returns>
  public AuthenticationDTO CreateToken(TokenDTO tokenDTO)
  {
    // expires in 10 minutes
    var expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration
                                                    ["JWTSettings:ExpirationMinutes"]));

    //create the JWT payload
    var claims = new Claim[ ]
    {
        //Subject (user id)
        new(JwtRegisteredClaimNames.Sub, tokenDTO.Id.ToString()),

        //JWT unique ID
        new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

        //Issued at (date and time of token generation)
        new(JwtRegisteredClaimNames.Iat,
            DateTime.UtcNow
                    .ToString()),

        //Unique name identifier of the user (Email)
        new(ClaimTypes.NameIdentifier,
            tokenDTO.Email),

        //Name of the user
        new(ClaimTypes.Name, tokenDTO.DisplayName),

        //Name of the user
        new(ClaimTypes.Email, tokenDTO.Email)
    };

    // get Key from appsettings.json
    var secretKey =
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

    // use Key to sign the JWT
    var signingCredentials =
        new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

    // create the JWT

    // issue : means that key is issued by it
    // audience : means that the key is intended for this application
    var jwt = new JwtSecurityToken(_configuration["Jwt:Issuer"]!,
                                   _configuration["Jwt:Audience"]!,
                                   claims,
                                   expires: expiration,
                                   signingCredentials: signingCredentials);

    // Create the token
    var token = new JwtSecurityTokenHandler().WriteToken(jwt);

    // return the token
    return new AuthenticationDTO
    {
        Token = token,
        DisplayName = tokenDTO.DisplayName,
        Email = tokenDTO.Email,
        Expiration = expiration,
        RefreshToken = GenerateRefreshToken(),
        RefreshTokenExpirationDateTime =
            DateTime.Now.AddMinutes(Convert.ToInt32(_configuration
                                                        ["RefreshToken:EXPIRATION_MINUTES"]))
    };
  }

  public ClaimsPrincipal? GetPrincipalFromJwtToken(string? token)
  {
    var tokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidAudience = _configuration["Jwt:Audience"]!,
        ValidateIssuer = true,
        ValidIssuer = _configuration["Jwt:Issuer"]!,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8
                                             .GetBytes(_configuration["Jwt:Key"]
                                                       !)),
        ValidateLifetime = false // should be false here
    };

    var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

    // 对 token 进行验证
    var principal =
        jwtSecurityTokenHandler.ValidateToken(token,
                                              tokenValidationParameters,
                                              out var securityToken);

    // 如果token的类型不是JwtSecurityToken
    // 并且，验证不通过
    // 则抛出异常
    if (securityToken is not JwtSecurityToken jwtSecurityToken
     || !jwtSecurityToken
         .Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                            StringComparison.InvariantCulture))
    {
      throw new SecurityTokenValidationException("Invalid token");
    }

    return principal;
  }

  private string GenerateRefreshToken()
  {
    var bytes = new byte[64];
    var randomNumberGenerator = RandomNumberGenerator.Create();
    randomNumberGenerator.GetBytes(bytes);

    return Convert.ToBase64String(bytes);
  }
}
