using Application.common.DTO;

namespace Application.common.Interfaces;

public interface IUserService
{
  Task<bool> IsUserExistingAsync(string email);

  Task<UserDto?> GetUserByIdAsync(string userId, CancellationToken cancellationToken);

  Task<UserDto> GetUserByEmailAsync(string username, CancellationToken cancellationToken);

  Task<IEnumerable<UserDto>> GetAllUsersAsync();

  Task<IEnumerable<UserDto>> GetUsersByIdsAsync(IEnumerable<string> userIds, CancellationToken cancellationToken);

  Task<IEnumerable<UserDto>> SearchUsersAsync(string searchTerm);

  Task AddUserAsync(UserDto userDto);

  Task UpdateUserAsync(UserDto userDto);

  Task DeleteUserAsync(Guid userId);

  Task<bool> ValidateUserCredentialsAsync(string username, string password);

  Task ChangePasswordAsync(Guid userId, string oldPassword, string newPassword);

  Task ResetPasswordAsync(Guid userId);

  Task<bool> IsUserInRoleAsync(Guid userId, string roleName);

  Task AddUserToRoleAsync(Guid userId, string roleName);

  Task RemoveUserFromRoleAsync(Guid userId, string roleName);
}
