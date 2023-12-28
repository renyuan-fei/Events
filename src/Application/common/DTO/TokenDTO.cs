namespace Application.common.DTO;

/// <summary>
///   Represents a Data Transfer Object for a token.
/// </summary>
public class TokenDTO
{
  /// <summary>
  ///   Gets or sets the unique identifier for the property.
  /// </summary>
  /// <value>
  ///   A <see cref="Guid" /> representing the property Id.
  /// </value>
  public string Id { get; set; }

  /// <summary>
  ///   Gets or sets the username of a user.
  /// </summary>
  public string UserName { get; set; }

  /// <summary>
  ///   Gets or sets the email value.
  /// </summary>
  /// <value>The email value.</value>
  public string Email { get; set; }

  /// <summary>
  ///   Gets or sets the display name.
  /// </summary>
  /// <value>
  ///   The display name.
  /// </value>
  public string DisplayName { get; set; }
}
