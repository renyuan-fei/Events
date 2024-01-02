using Application.common.DTO;

namespace Application.common.Interfaces;

public interface IUserService
{
  Task<UserDTO> GetUserByIdAsync(string userId);

  Task<UserDTO> GetUserByEmailAsync(string username);

  Task<IEnumerable<UserDTO>> GetAllUsersAsync();

  Task<IEnumerable<UserDTO>> GetUsersByIdsAsync(IEnumerable<string> userIds);

  Task<IEnumerable<UserDTO>> SearchUsersAsync(string searchTerm);

  Task AddUserAsync(UserDTO userDto);

  Task UpdateUserAsync(UserDTO userDto);

  Task DeleteUserAsync(Guid userId);

  Task<bool> ValidateUserCredentialsAsync(string username, string password);

  Task ChangePasswordAsync(Guid userId, string oldPassword, string newPassword);

  Task ResetPasswordAsync(Guid userId);

  Task<bool> IsUserInRoleAsync(Guid userId, string roleName);

  Task AddUserToRoleAsync(Guid userId, string roleName);

  Task RemoveUserFromRoleAsync(Guid userId, string roleName);
}
