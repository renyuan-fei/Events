namespace Application.common.DTO;

/// <summary>
///   Represents a data transfer object for a user.
/// </summary>
public class UserDTO
{
  /// <summary>
  ///   Gets or sets the display name.
  /// </summary>
  public string DisplayName { get; set; }

  /// <summary>
  ///   Gets or sets the token.
  /// </summary>
  /// <value>
  ///   The token.
  /// </value>
  public string Token { get; set; }

  /// <summary>
  ///   Gets or sets the image property.
  /// </summary>
  /// <value>The image as a string.</value>
  public string Image { get; set; }

  /// <summary>
  ///   Gets or sets the username.
  /// </summary>
  /// <value>
  ///   The username.
  /// </value>
  public string Username { get; set; }
}
