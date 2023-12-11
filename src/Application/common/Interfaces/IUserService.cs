using Application.common.DTO;

namespace Application.common.Interfaces;

/// <summary>
/// Represents a service for retrieving user information.
/// </summary>
public interface IUserService
{
  /// <summary>
  /// Retrieves user information based on the user ID.
  /// </summary>
  /// <param name="userId">The unique identifier of the user.</param>
  /// <returns>The UserInfoDTO object containing the user information.</returns>
  public Task<UserInfoDTO> GetUserInfoByIdAsync(Guid userId);

  /// <summary>
  /// Retrieves information for a list of users based on their IDs asynchronously.
  /// </summary>
  /// <param name="userIds">The list of user IDs.</param>
  /// <returns>A task that represents the asynchronous operation. The task result contains a list of UserInfoDTO objects.</returns>
  Task<List<UserInfoDTO>> GetUsersInfoByIdsAsync(List<Guid> userIds);

  /// <summary>
  /// Retrieves the user information by email.
  /// </summary>
  /// <param name="email">The email of the user.</param>
  /// <returns>The user information as a UserInfoDTO object.</returns>
  public Task<UserInfoDTO> GetUserInfoByEmailAsync(string email);

  /// <summary>
  /// Retrieves user information based on the given phone number.
  /// </summary>
  /// <param name="phoneNumber">The phone number of the user.</param>
  /// <returns>A Task containing the UserInfoDTO object representing the user information.</returns>
  public Task<UserInfoDTO> GetUserInfoByPhoneNumberAsync(string phoneNumber);
}
