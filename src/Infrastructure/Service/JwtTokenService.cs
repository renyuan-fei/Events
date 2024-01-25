using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using Application.common.DTO;
using Application.common.interfaces;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Service;

public class JwtTokenService : IJwtTokenService
{
  private readonly IConfiguration _configuration;

  public JwtTokenService(IConfiguration configuration) { _configuration = configuration; }

  public AuthenticationDto CreateToken(TokenDto tokenDTO, bool isRefreshToken = false)
  {
    // expires in 10 minutes
    var expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration
                                                    ["Jwt:EXPIRATION_MINUTES"]));

    //create the JWT payload
    var claims = new Claim[ ]
    {
        //Subject (user id)
        new(JwtRegisteredClaimNames.Sub, tokenDTO.Id),

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
    if (isRefreshToken)
    {
      return new AuthenticationDto
      {
          Token = token,
          Expiration = expiration,
          RefreshToken = GenerateRefreshToken(),
          RefreshTokenExpirationDateTime =
              DateTime.Now.AddMinutes(Convert.ToInt32(_configuration
                                                          ["RefreshToken:EXPIRATION_MINUTES"]))
      };
    }

    return new AuthenticationDto
    {
            Token = token,
            Expiration = expiration,
    };
  }

  public ClaimsPrincipal? GetPrincipalFromJwtToken(string? token, bool validateLifetime = true)
  {
    if (string.IsNullOrEmpty(token))
    {
      return null;
    }

    var tokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidIssuer = _configuration["Jwt:Issuer"],
        ValidateAudience = true,
        ValidAudience = _configuration["Jwt:Audience"],
        ValidateLifetime = validateLifetime, // 可以根据需要设置为 true 或 false
        ClockSkew = TimeSpan.Zero            // 可以根据需要设置时钟偏移量
    };

    try
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);

      // 验证是否为 JWT
      if (validatedToken is JwtSecurityToken jwtSecurityToken)
      {
        if (jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
          return principal;
        }
      }
    }
    catch (Exception)
    {
      // 在这里处理验证失败的情况，例如记录日志等
      return null;
    }

    return null;
  }

  /// <summary>
  ///   Generates a refresh token.
  /// </summary>
  /// <returns>A string containing the generated refresh token.</returns>
  private string GenerateRefreshToken()
  {
    var bytes = new byte[64];
    var randomNumberGenerator = RandomNumberGenerator.Create();
    randomNumberGenerator.GetBytes(bytes);

    return Convert.ToBase64String(bytes);
  }
}
