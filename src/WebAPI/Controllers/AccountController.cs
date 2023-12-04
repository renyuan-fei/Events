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
  public AccountController(
      UserManager<ApplicationUser>   userManager,
      SignInManager<ApplicationUser> signInManager,
      IIdentityService               identityService,
      IJwtTokenService               jwtTokenService)
  {
    _userManager = userManager;
    _signInManager = signInManager;
    _identityService = identityService;
    _jwtTokenService = jwtTokenService;
  }

  /// <summary>
  /// </summary>
  /// <param name="registerDTO"></param>
  /// <returns></returns>
  [ HttpPost("register") ]
  public async Task<ActionResult<AuthenticationResponse>> Register(
      [ FromBody ] RegisterDTO registerDTO)
  {
    if (!ModelState.IsValid) { return BadRequest(ModelState); }

    var user = new ApplicationUser
    {
        UserName = registerDTO.Email,
        Email = registerDTO.Email,
        DisplayName = registerDTO.DisplayName
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
  public async Task<ActionResult<AuthenticationResponse>> Login(
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
  /// <param name="tokenModel"></param>
  /// <returns></returns>
  [ HttpPost("refresh-token") ]
  public async Task<ActionResult<AuthenticationResponse>> RefreshToken(
      [ FromBody ] TokenModel tokenModel)
  {
    var user =
        await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshToken
                                                        == tokenModel.RefreshToken);

    if (user == null
     || user.RefreshTokenExpirationDateTime <= DateTime.Now)
    {
      return BadRequest("Invalid refresh token.");
    }

    return await GenerateTokenResponse(user);
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

  async private Task<ActionResult<AuthenticationResponse>> GenerateTokenResponse(
      ApplicationUser user)
  {
    var token = _jwtTokenService.CreateToken(user);
    user.RefreshToken = token.RefreshToken;
    user.RefreshTokenExpirationDateTime = token.RefreshTokenExpirationDateTime;
    await _userManager.UpdateAsync(user);

    return Ok(token);
  }
}
