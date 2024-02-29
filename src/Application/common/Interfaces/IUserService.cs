using Application.common.DTO;

namespace Application.common.Interfaces;

public interface IUserService
{
  Task<bool> IsUserExistingAsync(string email);

  Task<UserDto?> GetUserByIdAsync(string userId, CancellationToken cancellationToken);

  Task<UserDto> GetUserByEmailAsync(string username, CancellationToken cancellationToken);

  Task<IEnumerable<UserDto>> GetUsersByIdsAsync(IEnumerable<string> userIds, CancellationToken cancellationToken);

  Task UpdateUserAsync(UserDto userDto);
}
