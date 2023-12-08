using Application.common.DTO;

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
  /// <returns>
  ///   The <see cref="UserInfoDTO" /> object containing the user information.
  /// </returns>
  public Task<UserInfoDTO> GetUserInfoByIdAsync(Guid userId);

  /// <summary>
  ///   Retrieves the user information by email.
  /// </summary>
  /// <param name="email">The email of the user.</param>
  /// <returns>
  ///   The user information as a UserInfoDTO object.
  /// </returns>
  /// /
  public Task<UserInfoDTO> GetUserInfoByEmailAsync(string email);

  /// <summary>
  ///   Retrieves the user information associated with the given phone number.
  /// </summary>
  /// <param name="phoneNumber">The phone number of the user.</param>
  /// <returns>
  ///   A <see cref="UserInfoDTO" /> object containing the user information.
  /// </returns>
  public Task<UserInfoDTO> GetUserInfoByPhoneNumberAsync(string phoneNumber);
}
