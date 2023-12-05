using System.Security.Claims;

using Application.common.DTO;
using Application.common.interfaces;

using Infrastructure.Identity;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers;

/// <summary>
/// </summary>
[ AllowAnonymous ]
public class AccountController : BaseController
{
  private readonly ICurrentUserService            _currentUserService;
  private readonly IIdentityService               _identityService;
  private readonly IJwtTokenService               _jwtTokenService;
  private readonly SignInManager<ApplicationUser> _signInManager;
  private readonly UserManager<ApplicationUser>   _userManager;

  /// <summary>
  /// </summary>
  /// <param name="userManager"></param>
  /// <param name="signInManager"></param>
  /// <param name="identityService"></param>
  /// <param name="jwtTokenService"></param>
  /// <param name="currentUserService"></param>
  public AccountController(
      UserManager<ApplicationUser>   userManager,
      SignInManager<ApplicationUser> signInManager,
      IIdentityService               identityService,
      IJwtTokenService               jwtTokenService,
      ICurrentUserService            currentUserService)
  {
    _userManager = userManager;
    _signInManager = signInManager;
    _identityService = identityService;
    _jwtTokenService = jwtTokenService;
    _currentUserService = currentUserService;
  }

  /// <summary>
  /// </summary>
  /// <param name="registerDTO"></param>
  /// <returns></returns>
  [ HttpPost("register") ]
  public async Task<ActionResult<AccountResponseDTO>> Register(
      [ FromBody ] RegisterDTO registerDTO)
  {
    if (!ModelState.IsValid) { return BadRequest(ModelState); }

    var user = new ApplicationUser
    {
        UserName = registerDTO.Email,
        Email = registerDTO.Email,
        DisplayName = registerDTO.DisplayName,
        PhoneNumber = registerDTO.PhoneNumber
    };

    var result = await _userManager.CreateAsync(user, registerDTO.Password);
    if (!result.Succeeded) { return BadRequest(result.Errors); }

    return await GenerateTokenResponse(user);
  }

  /// <summary>
  /// </summary>
  /// <param name="loginDTO"></param>
  /// <returns></returns>
  [ HttpPost("login") ]
  public async Task<ActionResult<AccountResponseDTO>> Login(
      [ FromBody ] LoginDTO loginDTO)
  {
    if (!ModelState.IsValid) { return BadRequest(ModelState); }

    var result =
        await _signInManager.PasswordSignInAsync(loginDTO.Email,
                                                 loginDTO.Password,
                                                 false,
                                                 false);

    if (!result.Succeeded) { return Unauthorized("Invalid login attempt."); }

    var user = await _userManager.FindByEmailAsync(loginDTO.Email);

    return await GenerateTokenResponse(user);
  }

  /// <summary>
  /// </summary>
  /// <returns></returns>
  [ HttpPost("refresh") ]
  public async Task<IActionResult> GenerateNewAccessToken()
  {
    // 从HTTP请求的Cookie中获取令牌
    var jwtToken = Request.Cookies["JwtToken"];
    var refreshToken = Request.Cookies["RefreshToken"];

    // 检查令牌是否为空
    if (string.IsNullOrEmpty(jwtToken)
     || string.IsNullOrEmpty(refreshToken))
    {
      return BadRequest("Invalid token request");
    }

    // 验证JWT令牌
    ClaimsPrincipal? principal = _jwtTokenService.GetPrincipalFromJwtToken(jwtToken);

    // 检查JWT令牌是否无效
    if (principal == null) { return BadRequest("Invalid JWT token"); }

    // 从JWT令牌中获取用户信息
    var email = principal.FindFirstValue(ClaimTypes.Email);

    // 根据电子邮件获取用户
    var user = await _userManager.FindByEmailAsync(email);

    // 验证刷新令牌和过期时间
    if (user == null
     || user.RefreshToken != refreshToken
     || user.RefreshTokenExpirationDateTime <= DateTime.Now)
    {
      return BadRequest("Invalid refresh token");
    }

    var data = new TokenDTO
    {
        DisplayName = user.DisplayName,
        Email = user.Email,
        UserName = user.UserName,
        Id = user.Id,
    };

    // 生成新的JWT令牌
    var token = _jwtTokenService.CreateToken(data);

    // 更新用户的刷新令牌信息
    user.RefreshToken = token.RefreshToken;
    user.RefreshTokenExpirationDateTime = token.RefreshTokenExpirationDateTime;
    await _userManager.UpdateAsync(user);

    // 更新HTTP Only Cookie
    var cookieOptions = new CookieOptions
    {
        HttpOnly = true,
        Expires = DateTime.UtcNow.AddMinutes(60), // 设置合适的过期时间
        Secure = true                             // 如果使用HTTPS，建议设置为true
    };

    Response.Cookies.Append("JwtToken", token.Token!, cookieOptions);

    Response.Cookies.Append("RefreshToken",
                            token.RefreshToken!,
                            new CookieOptions
                            {
                                HttpOnly = true,
                                Expires = token.RefreshTokenExpirationDateTime,
                                Secure = true
                            });

    // 返回确认信息或空内容
    return Ok(new { message = "Token refreshed successfully" });
  }

  /// <summary>
  /// </summary>
  /// <returns></returns>
  [ HttpGet("logout") ]
  public async Task<IActionResult> Logout()
  {
    await _signInManager.SignOutAsync();

    return NoContent();
  }

  async private Task<ActionResult<AccountResponseDTO>> GenerateTokenResponse(
      ApplicationUser user)
  {
    var tokenDto = new TokenDTO
    {
        Id = user.Id,
        DisplayName = user.DisplayName,
        Email = user.Email,
        UserName = user.UserName
    };

    var token = _jwtTokenService.CreateToken(tokenDto);

    // 设置HTTP Only Cookie
    var cookieOptions = new CookieOptions
    {
        HttpOnly = true,
        Expires = DateTime.UtcNow.AddMinutes(60), // 设置合适的过期时间
        Secure = true                             // 如果使用HTTPS，建议设置为true
    };

    // 将JWT Token写入Cookie
    Response.Cookies.Append("JwtToken", token.Token!, cookieOptions);

    // 可选：如果需要刷新令牌，也可以设置为另一个Cookie
    Response.Cookies.Append("RefreshToken",
                            token.RefreshToken!,
                            new CookieOptions
                            {
                                HttpOnly = true,
                                Expires = token.RefreshTokenExpirationDateTime,
                                Secure = true // 如果使用HTTPS，建议设置为true
                            });

    // 更新用户刷新令牌信息
    user.RefreshToken = token.RefreshToken;
    user.RefreshTokenExpirationDateTime = token.RefreshTokenExpirationDateTime;
    await _userManager.UpdateAsync(user);

    // 创建返回的DTO
    var responseDto = new AccountResponseDTO
    {
        DisplayName = user.DisplayName, Email = user.Email,
        // 可以添加其他必要的信息
    };

    return Ok(responseDto);
  }
}
