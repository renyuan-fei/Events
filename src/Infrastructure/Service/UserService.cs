using Application.common.DTO;
using Application.common.Interfaces;

using AutoMapper;

using Infrastructure.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Service;

public class UserService : IUserService
{
  private readonly ILogger<UserService>         _logger;
  private readonly IMapper                      _mapper;
  private readonly UserManager<ApplicationUser> _userManager;

  public UserService(
      UserManager<ApplicationUser> userManager,
      ILogger<UserService>         logger,
      IMapper                      mapper)
  {
    _userManager = userManager;
    _logger = logger;
    _mapper = mapper;
  }

  public async Task<bool> IsUserExistingAsync(string email)
  {
    return await _userManager.Users.AnyAsync(u => u.Email == email);
  }

  public async Task<UserDto?> GetUserByIdAsync(string userId, CancellationToken cancellationToken)
  {
    var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
    Guard.Against.Null(user, message: $"User with Id {userId} not found.");

    return _mapper.Map<UserDto>(user);
  }

  public async Task<UserDto> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
  {
    var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    Guard.Against.Null(user, message: $"User with Email {email} not found.");

    return _mapper.Map<UserDto>(user);
  }

  public Task<IEnumerable<UserDto>> GetAllUsersAsync()
  {
    throw new NotImplementedException();
  }

  public async Task<IEnumerable<UserDto>> GetUsersByIdsAsync(IEnumerable<string> userIds, CancellationToken cancellationToken)
  {
    var enumerable = userIds.ToList();
    Guard.Against.NullOrEmpty(enumerable, message: "User IDs list is null or empty.");

    try
    {
      var users = await _userManager.Users
                                    .Where(u => Enumerable.Contains(enumerable, u.Id))
                                    .ToListAsync(cancellationToken);

      return users.Select(user => _mapper.Map<UserDto>(user));
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "ErrorMessage Mapping to DTO: {ExMessage}", ex.Message);

      throw new ApplicationException("An error occurred while retrieving users.", ex);
    }
  }

  public Task<IEnumerable<UserDto>> SearchUsersAsync(string searchTerm)
  {
    throw new NotImplementedException();
  }

  public Task AddUserAsync(UserDto userDto) { throw new NotImplementedException(); }

  public async Task UpdateUserAsync(UserDto userDto)
  {
    var user = await _userManager.FindByIdAsync(userDto.Id);
    Guard.Against.Null(user, nameof(userDto.Id), "User to update not found.");

    // Map the DTO to the user entity and apply the updated values
    _mapper.Map(userDto, user);

    // Attempt to update the user with the new values
    var result = await _userManager.UpdateAsync(user);
    if (!result.Succeeded)
    {
      _logger.LogError("User update failed: {Errors}", result.Errors);
      throw new InvalidOperationException("Could not update user.");
    }

    _logger.LogInformation("User {UserId} updated successfully", user.Id);
  }

  public Task DeleteUserAsync(Guid userId) { throw new NotImplementedException(); }

  public Task<bool> ValidateUserCredentialsAsync(string username, string password)
  {
    throw new NotImplementedException();
  }

  public Task ChangePasswordAsync(Guid userId, string oldPassword, string newPassword)
  {
    throw new NotImplementedException();
  }

  public Task ResetPasswordAsync(Guid userId) { throw new NotImplementedException(); }

  public Task<bool> IsUserInRoleAsync(Guid userId, string roleName)
  {
    throw new NotImplementedException();
  }

  public Task AddUserToRoleAsync(Guid userId, string roleName)
  {
    throw new NotImplementedException();
  }

  public Task RemoveUserFromRoleAsync(Guid userId, string roleName)
  {
    throw new NotImplementedException();
  }
}
