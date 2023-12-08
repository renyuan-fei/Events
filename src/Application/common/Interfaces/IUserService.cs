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
  /// <returns>
  /// The <see cref="UserInfoDTO"/> object containing the user information.
  /// </returns>
  public Task<UserInfoDTO> GetUserInfoByIdAsync(Guid userId);

  /// <summary>
  /// Retrieves the user information by email.
  /// </summary>
  /// <returns>
  /// The user information as a UserInfoDTO object.
  /// </returns>
  public Task<UserInfoDTO> GetUserInfoByEmailAsync(string email);

  /// <summary>
  /// Retrieves the user information associated with the given phone number.
  /// </summary>
  /// <returns>
  /// A <see cref="UserInfoDTO"/> object containing the user information.
  /// </returns>
  public Task<UserInfoDTO> GetUserInfoByPhoneNumberAsync(string phoneNumber);
}
