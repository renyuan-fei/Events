namespace Application.common.DTO;

/// <summary>
///   Represents authentication data transfer object.
/// </summary>
public class AuthenticationDTO
{
  /// <summary>
  ///   Gets or sets the value of the token.
  /// </summary>
  /// <remarks>
  ///   This property represents a token used for authentication or authorization purposes.
  /// </remarks>
  /// <value>
  ///   The token value, or an empty string if not set.
  /// </value>
  public string? Token { get; set; } = string.Empty;

  /// <summary>
  ///   Gets or sets the expiration date of the property.
  /// </summary>
  public DateTime Expiration { get; set; }

  /// <summary>
  ///   Gets or sets the refresh token.
  /// </summary>
  /// <remarks>
  ///   This property holds the refresh token used for authentication purposes.
  /// </remarks>
  /// <value>The refresh token. If not set, the default value is an empty string.</value>
  public string? RefreshToken { get; set; } = string.Empty;

  /// <summary>
  ///   Gets or sets the date and time when the refresh token will expire.
  /// </summary>
  /// <value>
  ///   The refresh token expiration date and time.
  /// </value>
  public DateTime RefreshTokenExpirationDateTime { get; set; }

  public string MainPhoto { get; set; } = string.Empty;
}
