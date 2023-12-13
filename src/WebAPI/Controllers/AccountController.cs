using System.Security.Claims;

using Application.common.DTO;
using Application.common.interfaces;
using Application.Common.Interfaces;

using Infrastructure.Identity;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

/// <summary>
///   This class contains the account operations accessible by users. It extends from
///   BaseController.
/// </summary>
public class AccountController : BaseController
{
  private readonly ICurrentUserService            _currentUserService;
  private readonly IIdentityService               _identityService;
  private readonly IJwtTokenService               _jwtTokenService;
  private readonly SignInManager<ApplicationUser> _signInManager;
  private readonly UserManager<ApplicationUser>   _userManager;
  private readonly IEventsDbContext               _eventsDbContext;

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
  /// <param name="currentUserService">
  ///   A service for fetching the current authenticated user's
  ///   details
  /// </param>
  public AccountController(
      UserManager<ApplicationUser>   userManager,
      SignInManager<ApplicationUser> signInManager,
      IIdentityService               identityService,
      IJwtTokenService               jwtTokenService,
      ICurrentUserService            currentUserService,
      IEventsDbContext               eventsDbContext)
  {
    _userManager = userManager;
    _signInManager = signInManager;
    _identityService = identityService;
    _jwtTokenService = jwtTokenService;
    _currentUserService = currentUserService;
    _eventsDbContext = eventsDbContext;
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

    // 使用IdentityService的CreateUserAsync方法来创建用户
    var (result, userId) = await _identityService.CreateUserAsync(registerDTO.Email,
      registerDTO.DisplayName,
      registerDTO.PhoneNumber,
      registerDTO.Password);

    if (!result.Succeeded) { return BadRequest(result.Errors); }

    // 获取创建的用户
    var user = await _userManager.FindByIdAsync(userId.ToString());

    if (user == null)
    {
      return BadRequest("User creation succeeded but user not found.");
    }

    // 生成令牌响应
    return await GenerateTokenResponse(user);
  }

  /// <summary>
  ///   Login endpoint for authenticating users.
  /// </summary>
  /// <param name="loginDTO">Data Transfer Object containing the user's details for login.</param>
  /// <returns>
  ///   Returns an ActionResult instance of the AccountResponseDTO in JSON format.
  /// </returns>
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
  ///   Endpoint for generating a new access token.
  /// </summary>
  /// <returns>
  ///   Returns a IActionResult, with HTTP 200 status code and a success message if the request
  ///   was successful.
  /// </returns>
  [ HttpPost("refresh") ]
  public async Task<ActionResult<AccountResponseDTO>> GenerateNewAccessToken()
  {
    const string bearerPrefix = "Bearer ";

    // 从HTTP请求的Cookie中获取令牌
    var authHeader = Request.Headers["Authorization"].FirstOrDefault();

    var jwtToken = authHeader?.StartsWith(bearerPrefix) == true
        ? authHeader.Substring(bearerPrefix.Length).Trim()
        : null;

    var refreshToken = Request.Cookies["RefreshToken"];

    // 检查令牌是否为空
    if (string.IsNullOrEmpty(jwtToken)
     || string.IsNullOrEmpty(refreshToken))
    {
      return BadRequest("Invalid token request");
    }

    // 验证JWT令牌
    var principal = _jwtTokenService.GetPrincipalFromJwtToken(jwtToken);

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
        Id = user.Id
    };

    // 生成新的Access令牌, 但不更新过期时间
    var token = _jwtTokenService.CreateToken(data, true);

    // 更新HTTP Only Cookie

    // 返回确认信息或空内容
    return Ok(new AccountResponseDTO
    {
        DisplayName = user.DisplayName,
        Email = user.Email,
        Token = token.Token,
        ExpirationDateTime = token.Expiration
    });
  }

  /// <summary>
  ///   Logout endpoint for signing out the user.
  /// </summary>
  /// <returns>
  ///   Returns an empty IActionResult with HTTP 204 status code.
  /// </returns>
  [ Authorize ]
  [ HttpGet("logout") ]
  public async Task<IActionResult> Logout()
  {
    //Sign out the user from ASP.NET Core Authentication system.
    await _signInManager.SignOutAsync();

    //Delete the refresh token cookie
    Response.Cookies.Delete("RefreshToken");

    //set Token and ExpirationDateTime to null in database

    //Return NoContent Result (HTTP 204 Status Code)
    return NoContent();
  }

  /// <summary>
  ///   Internal method for generating a response token.
  /// </summary>
  /// <param name="user">Instance of the ApplicationUser class</param>
  /// <returns>
  ///   Returns an ActionResult instance of the AccountResponseDTO in JSON format.
  /// </returns>
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

    // create JWT Token
    var token = _jwtTokenService.CreateToken(tokenDto);

    // set Refresh Token in Cookie
    var cookieOptions = new CookieOptions
    {
        HttpOnly = true, Expires = token.RefreshTokenExpirationDateTime, Secure = true
    };

    Response.Cookies.Append("RefreshToken", token.RefreshToken!, cookieOptions);

    // 舍 User's Refresh Token and Refresh Token Expiration DateTime'
    user.RefreshToken = token.RefreshToken;
    user.RefreshTokenExpirationDateTime = token.RefreshTokenExpirationDateTime;

    // update user's refresh token and expiration date time'
    await _userManager.UpdateAsync(user);

    var responseDto = new AccountResponseDTO
    {
        DisplayName = user.DisplayName,
        Email = user.Email,
        Token = token.Token,
        ExpirationDateTime = token.Expiration,
        Image = _eventsDbContext.Photos.FirstOrDefault(p => p.UserId == user.Id
                                                        && p.IsMain
                                                        == true)!
                                .Url
    };

    return Ok(responseDto);
  }
}
