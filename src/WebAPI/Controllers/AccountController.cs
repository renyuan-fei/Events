using System.Security.Claims;

using Application.common.DTO;
using Application.common.interfaces;
using Application.common.Interfaces;

using Domain.Constant;
using Domain.Repositories;

using Infrastructure.Identity;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers;

/// <summary>
///   This class contains the account operations accessible by users. It extends from
///   BaseController.
/// </summary>
public class AccountController : BaseController
{
  private readonly IPhotoRepository               _photoRepository;
  private readonly IUserService                   _userService;
  private readonly IIdentityService               _identityService;
  private readonly IJwtTokenService               _jwtTokenService;
  private readonly SignInManager<ApplicationUser> _signInManager;
  private readonly UserManager<ApplicationUser>   _userManager;

  /// <summary>
  ///   This is the constructor method of the AccountController class. It contains all the
  ///   dependency injections necessary for the account operations.
  /// </summary>
  /// <param name="userManager">
  ///   UserManager class for managing instances of the IdentityUser
  ///   class
  /// </param>
  /// <param name="signInManager">
  ///   SignInManager class for managing instances of the SignIn
  ///   operations
  /// </param>
  /// <param name="identityService">A service providing identity related utility methods</param>
  /// <param name="jwtTokenService">A service for JWT token generation and validation</param>
  /// <param name="userService"></param>
  /// <param name="photoRepository"></param>
  public AccountController(
      UserManager<ApplicationUser>   userManager,
      SignInManager<ApplicationUser> signInManager,
      IIdentityService               identityService,
      IJwtTokenService               jwtTokenService,
      IUserService                   userService,
      IPhotoRepository               photoRepository)
  {
    _userManager = userManager;
    _signInManager = signInManager;
    _identityService = identityService;
    _jwtTokenService = jwtTokenService;
    _userService = userService;
    _photoRepository = photoRepository;
  }

  /// <summary>
  ///   Register endpoint for creating new users.
  /// </summary>
  /// <param name="registerDTO">
  ///   Data Transfer Object containing the user's details for
  ///   registration.
  /// </param>
  /// <returns>
  ///   Returns an ActionResult instance of the AccountResponseDTO in JSON format.
  /// </returns>
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

    if (await _userManager.FindByEmailAsync(registerDTO.Email) != null)
    {
      return BadRequest("Email is already taken.");
    }

    var result = await _userManager.CreateAsync(user, registerDTO.Password);

    if (!result.Succeeded) { return BadRequest(result.Errors); }

    return await GenerateTokenResponse(user);
  }

  /// <summary>
  ///   Login endpoint for authenticating users.
  /// </summary>
  /// <param name="loginDTO">Data Transfer Object containing the user's details for login.</param>
  /// <returns>
  ///   Returns an ActionResult instance of the AccountResponseDTO in JSON format.
  /// </returns>
  [HttpPost("login")]
  public async Task<ActionResult<AccountResponseDTO>> Login([FromBody] LoginDTO loginDTO)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, false, false);
    if (!result.Succeeded)
    {
      return Unauthorized("Invalid login attempt.");
    }

    var user = await _userManager.FindByEmailAsync(loginDTO.Email);
    if (user == null)
    {
      return Unauthorized("User not found.");
    }

    if (user.RefreshTokenExpirationDateTime != DateTime.MinValue && user.RefreshToken!= null)
    {
      return BadRequest("user is already logged in.");
    }

    // 确保所有必要的用户信息都存在
    if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.UserName))
    {
      return Unauthorized("User information is incomplete.");
    }

    // 登录成功，生成带有更新刷新令牌的响应
    return await GenerateTokenResponse(user, updateRefreshToken: true);
  }

  [Authorize]
  [HttpGet]
  public async Task<ActionResult<AccountResponseDTO>> GetCurrentUser()
  {
    var email = User.FindFirstValue(ClaimTypes.Email);
    var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
    if (user == null)
    {
      return Unauthorized("User not found.");
    }

    if (user.RefreshTokenExpirationDateTime== DateTime.MinValue && user.RefreshToken== null)
    {
      return Unauthorized("user is not logged in.");
    }

    // 确保所有必要的用户信息都存在
    if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.UserName))
    {
      return Unauthorized("User information is incomplete.");
    }

    // 获取当前用户信息，不更新刷新令牌
    return await GenerateTokenResponse(user, updateRefreshToken: false);
  }

  /// <summary>
  ///   Endpoint for generating a new access token.
  /// </summary>
  /// <returns>
  ///   Returns a IActionResult, with HTTP 200 status code and a success message if the request
  ///   was successful.
  /// </returns>
  [HttpPost("refresh")]
  public async Task<ActionResult<AccountResponseDTO>> GenerateNewAccessToken()
  {
    const string bearerPrefix = "Bearer ";

    // 从HTTP请求的Header中获取令牌
    var authHeader = Request.Headers["Authorization"].FirstOrDefault();

    var jwtToken = authHeader?.StartsWith(bearerPrefix) == true
        ? authHeader.Substring(bearerPrefix.Length).Trim()
        : null;

    var refreshToken = Request.Cookies["RefreshToken"];

    // 检查令牌是否为空
    if (string.IsNullOrEmpty(jwtToken) || string.IsNullOrEmpty(refreshToken))
    {
      return BadRequest("Invalid token request");
    }

    // 验证JWT令牌
    var principal = _jwtTokenService.GetPrincipalFromJwtToken(jwtToken, false);

    // 检查JWT令牌是否无效
    if (principal == null) { return BadRequest("Invalid JWT token"); }

    // 从JWT令牌中获取用户信息
    var email = principal.FindFirstValue(ClaimTypes.Email);

    // 根据电子邮件获取用户
    var user = await _userManager.FindByEmailAsync(email);

    // 验证刷新令牌和过期时间
    if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpirationDateTime <= DateTime.Now)
    {
      return BadRequest("Invalid refresh token");
    }

    // 生成新的Access令牌，同时更新刷新令牌
    return await GenerateTokenResponse(user, updateRefreshToken: true);
  }

  /// <summary>
  ///   Logout endpoint for signing out the user.
  /// </summary>
  /// <returns>
  ///   Returns an empty IActionResult with HTTP 204 status code.
  /// </returns>
  [Authorize]
  [HttpPost("logout")]
  public async Task<IActionResult> Logout()
  {
    var email = User.FindFirstValue(ClaimTypes.Email);
    var user = await _userManager.FindByEmailAsync(email);

    if (user == null)
    {
      return BadRequest("User not found");
    }

    if (user.RefreshToken == null && user.RefreshTokenExpirationDateTime == DateTime.MinValue)
    {
      return BadRequest("User is not logout.");
    }

    // 清除用户的刷新令牌和过期时间
    user.RefreshToken = null;
    user.RefreshTokenExpirationDateTime = DateTime.MinValue;
    await _userManager.UpdateAsync(user);

    // 从 ASP.NET Core 认证系统中登出用户
    await _signInManager.SignOutAsync();

    // 删除存储在客户端的刷新令牌 Cookie
    Response.Cookies.Delete("RefreshToken");

    // 返回无内容的结果（HTTP 204状态码）
    return NoContent();
  }

  /// <summary>
  ///   Internal method for generating a response token.
  /// </summary>
  /// <param name="user">Instance of the ApplicationUser class</param>
  /// <param name="updateRefreshToken"></param>
  /// <returns>
  ///   Returns an ActionResult instance of the AccountResponseDTO in JSON format.
  /// </returns>
  async private Task<ActionResult<AccountResponseDTO>> GenerateTokenResponse(ApplicationUser user, bool updateRefreshToken = false)
  {
    var tokenDto = new TokenDTO
    {
        Id = user.Id,
        DisplayName = user.DisplayName,
        Email = user.Email,
        UserName = user.UserName
    };

    // 创建 JWT 令牌
    var token = _jwtTokenService.CreateToken(tokenDto, updateRefreshToken);

    if (updateRefreshToken)
    {
      // 设置 Refresh Token 到 Cookie
      var cookieOptions = new CookieOptions
      {
          HttpOnly = true,
          Expires = token.RefreshTokenExpirationDateTime,
          Secure = true
      };
      Response.Cookies.Append("RefreshToken", token.RefreshToken!, cookieOptions);

      // 更新用户的 Refresh Token 和过期时间
      user.RefreshToken = token.RefreshToken;
      user.RefreshTokenExpirationDateTime = token.RefreshTokenExpirationDateTime;
      await _userManager.UpdateAsync(user);
    }

    // 获取用户的照片信息
    var image = await _photoRepository.GetMainPhotoByOwnerIdAsync(user.Id, CancellationToken.None);

    var responseDto = new AccountResponseDTO
    {
        DisplayName = user.DisplayName,
        Email = user.Email,
        Token = token.Token,
        ExpirationDateTime = token.Expiration,
        Image = image != null ? image.Details.Url : DefaultImage.DefaultUserImageUrl
    };

    return Ok(responseDto);
  }
}
