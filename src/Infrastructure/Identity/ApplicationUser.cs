using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

/// <summary>
///   Represents an application user.
/// </summary>
public class ApplicationUser : IdentityUser
{
  /// <summary>
  ///   Gets or sets the display name.
  /// </summary>
  public string DisplayName { get; set; }

  /// Summary: Gets or sets the biography of a person.
  /// Remarks: This property represents the biography of a person, which provides additional information about the person.
  /// The biography can be accessed using the get and set accessors.
  /// The bio can be null or empty if there is no biography available for the person.
  /// Example usage:
  /// // Set the biography of a person
  /// person.Bio = "John Doe is a talented software engineer with 5 years of experience.";
  /// // Get the biography of a person
  /// string bio = person.Bio;
  /// @property string? Bio
  /// The biography of a person.
  /// /
  public string? Bio { get; set; } = "Hello World!";

  /// <summary>
  ///   Gets or sets the refresh token for the authentication process.
  /// </summary>
  /// <remarks>
  ///   A refresh token is a unique string that is used to refresh an expired access token.
  ///   This property is nullable, meaning it can be assigned a null value if no refresh token
  ///   is available.
  /// </remarks>
  /// <value>
  ///   A string representing the refresh token.
  /// </value>
  public string? RefreshToken { get; set; }

  /// <summary>
  ///   Gets or sets the expiration date and time of the refresh token.
  /// </summary>
  /// <value>
  ///   The expiration date and time of the refresh token.
  /// </value>
  public DateTime RefreshTokenExpirationDateTime { get; set; }
}
