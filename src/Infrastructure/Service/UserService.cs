using Application.common.DTO;
using Application.common.Interfaces;

using AutoMapper;

using Domain.Repositories;

using Infrastructure.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Service;

public class UserService : IUserService
{
  private readonly IMapper                      _mapper;
  private readonly UserManager<ApplicationUser> _userManager;
  private readonly ILogger<UserService>         _logger;

  public UserService(
      UserManager<ApplicationUser> userManager,
      ILogger<UserService>         logger,
      IMapper                      mapper)
  {
    _userManager = userManager;
    _logger = logger;
    _mapper = mapper;
  }

  public async Task<UserDTO> GetUserByIdAsync(string userId)
  {
    var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
    Guard.Against.Null(user, message: $"User with Id {userId} not found.");
    return _mapper.Map<UserDTO>(user);
  }

  public async Task<UserDTO> GetUserByEmailAsync(string email)
  {
    var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
    Guard.Against.Null(user, message: $"User with Email {email} not found.");
    return _mapper.Map<UserDTO>(user);
  }

  public Task<IEnumerable<UserDTO>> GetAllUsersAsync()
  {
    throw new NotImplementedException();
  }

  public async Task<IEnumerable<UserDTO>> GetUsersByIdsAsync(IEnumerable<string> userIds)
  {
    var enumerable = userIds.ToList();
    Guard.Against.NullOrEmpty(enumerable, message:"User IDs list is null or empty.");

    try
    {
      var users = await _userManager.Users
                                    .Where(u => Enumerable.Contains(enumerable, u.Id))
                                    .ToListAsync();

      return users.Select(user => _mapper.Map<UserDTO>(user));
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "ErrorMessage Mapping to DTO: {ExMessage}", ex.Message);
      throw new ApplicationException("An error occurred while retrieving users.", ex);
    }
  }

  public Task<IEnumerable<UserDTO>> SearchUsersAsync(string searchTerm)
  {
    throw new NotImplementedException();
  }

  public Task AddUserAsync(UserDTO userDto) { throw new NotImplementedException(); }

  public Task UpdateUserAsync(UserDTO userDto) { throw new NotImplementedException(); }

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
