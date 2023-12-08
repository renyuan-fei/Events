namespace Application.common.DTO;

/// <summary>
///   Represents a data transfer object for user information.
/// </summary>
public class UserInfoDTO
{
  /// <summary>
  ///   Gets or sets the display name.
  /// </summary>
  /// <value>
  ///   The display name.
  /// </value>
  public string DisplayName { get; set; }

  /// <summary>
  ///   Gets or sets the bio of a person.
  /// </summary>
  /// <value>
  ///   The bio of the person.
  /// </value>
  public string Bio { get; set; }

  /// <summary>
  ///   Gets or sets the username.
  /// </summary>
  /// <value>
  ///   The username.
  /// </value>
  public string UserName { get; set; }

  /// <summary>
  ///   Gets or sets the email address.
  /// </summary>
  public string Email { get; set; }

  /// <summary>
  ///   Gets or sets the phone number.
  /// </summary>
  /// <value>
  ///   The phone number.
  /// </value>
  public string PhoneNumber { get; set; }
}
