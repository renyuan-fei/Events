using Application.common.DTO;
using Application.common.Models;

namespace Application.common.Interfaces;

/// <summary>
///   Represents a service for retrieving user information.
/// </summary>
public interface IUserService
{
  /// <summary>
  ///   Retrieves user information based on the user ID.
  /// </summary>
  /// <param name="userId">The unique identifier of the user.</param>
  /// <param name="includePhotos"></param>
  /// <returns>The UserInfoDTO object containing the user information.</returns>
  public Task<UserInfoDTO> GetUserInfoByIdAsync(Guid userId, bool includePhotos = false);

  /// <summary>
  ///   Retrieves information for a list of users based on their IDs asynchronously.
  /// </summary>
  /// <param name="userIds">The list of user IDs.</param>
  /// <returns>
  ///   A task that represents the asynchronous operation. The task result contains a
  ///   list of UserInfoDTO objects.
  /// </returns>
  Task<List<UserInfoDTO>> GetUsersInfoByIdsAsync(List<Guid> userIds);

  public Task<bool> UpdateUserInfoAsync(Guid userId, UserDTO userDTO);

  public Task<bool> IsUserExistsAsync(Guid userId);
}
