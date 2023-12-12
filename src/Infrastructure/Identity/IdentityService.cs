using Application.common.interfaces;
using Application.Common.Interfaces;
using Application.common.Models;

using Domain;
using Domain.Entities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity;

public class IdentityService : IIdentityService
{
  private readonly IAuthorizationService _authorizationService;

  private readonly IUserClaimsPrincipalFactory<ApplicationUser>
      _userClaimsPrincipalFactory;

  private readonly UserManager<ApplicationUser> _userManager;
  private readonly IEventsDbContext             _context;

  public IdentityService(
      UserManager<ApplicationUser>                 userManager,
      IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
      IAuthorizationService                        authorizationService,
      IEventsDbContext                             eventsDbContext)
  {
    _userManager = userManager;
    _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
    _authorizationService = authorizationService;
    _context = eventsDbContext;
  }

  public async Task<string?> GetUserNameAsync(Guid userId)
  {
    var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

    return user.UserName;
  }

  public async Task<(Result Result, Guid userId)> CreateUserAsync(
      string userName,
      string password)
  {
    var user = new ApplicationUser
    {
        UserName = userName, Email = userName, DisplayName = userName // 或其他适当的默认值
    };

    var result = await _userManager.CreateAsync(user, password);

    return (result.ToApplicationResult(), user.Id);
  }

  public async Task<(Result Result, Guid userId)> CreateUserAsync(
      string email,
      string displayName,
      string phoneNumber,
      string password)
  {
    const string defaultImage = "https://res.cloudinary.com/dxwtrnpqi/image/upload/v1702453913/gyzjw6xpz9pzl0xg7de4.jpg";
    const string defaultImagePublicId = "gyzjw6xpz9pzl0xg7de4";

    var user = new ApplicationUser
    {
        UserName = email,
        Email = email,
        DisplayName = displayName,
        PhoneNumber = phoneNumber
    };

    var result = await _userManager.CreateAsync(user, password);

    // 首先检查用户是否成功创建
    if (!result.Succeeded)
    {
      return (result.ToApplicationResult(), user.Id);
    }

    // 添加默认图片
    _context.Photos.Add(new Photo
    {
        PublicId = defaultImagePublicId,
        UserId = user.Id,
        IsMain = true,
        Url = defaultImage
    });

    // 保存DbContext的更改
    var saveResult = await _context.SaveChangesAsync(new CancellationToken());

    if (saveResult == 0)
    {
      throw new DbUpdateException("Could not save user with photo.");
    }

    return (result.ToApplicationResult(), user.Id);
  }

  public async Task<bool> IsInRoleAsync(Guid userId, string role)
  {
    var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

    return user != null && await _userManager.IsInRoleAsync(user, role);
  }

  public async Task<bool> AuthorizeAsync(Guid userId, string policyName)
  {
    var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

    if (user == null) { return false; }

    var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

    var result = await _authorizationService.AuthorizeAsync(principal, policyName);

    return result.Succeeded;
  }

  public async Task<Result> DeleteUserAsync(Guid userId)
  {
    var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

    return user != null
        ? await DeleteUserAsync(user)
        : Result.Success();
  }

  public async Task<Result> DeleteUserAsync(ApplicationUser user)
  {
    var result = await _userManager.DeleteAsync(user);

    return result.ToApplicationResult();
  }
}
