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
  public UserInfoDTO GetUserInfoById(Guid userId);

  /// <summary>
  /// Retrieves the user information by email.
  /// </summary>
  /// <returns>
  /// The user information as a UserInfoDTO object.
  /// </returns>
  public UserInfoDTO GetUserInfoByEmail(string email);

  /// <summary>
  /// Retrieves the user information associated with the given phone number.
  /// </summary>
  /// <returns>
  /// A <see cref="UserInfoDTO"/> object containing the user information.
  /// </returns>
  public UserInfoDTO GetUserInfoByPhoneNumber(string phoneNumber);
}
