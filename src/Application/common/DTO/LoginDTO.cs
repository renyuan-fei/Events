using System.ComponentModel.DataAnnotations;

namespace Application.common.DTO;

/// <summary>
///   Represents a data transfer object for login details.
/// </summary>
public class LoginDTO
{
  /// <summary>
  ///   Gets or sets the email address.
  /// </summary>
  [ Required(ErrorMessage = "Email is required") ]
  [ EmailAddress(ErrorMessage = "Email is not valid") ]
  public string Email { get; set; } = string.Empty;

  /// <summary>
  ///   Represents a password field.
  /// </summary>
  /// <remarks>
  ///   The password field is required and must be provided by the user.
  /// </remarks>
  /// <value>
  ///   The string value representing the password.
  /// </value>
  [ Required(ErrorMessage = "Password is required") ]
  public string Password { get; set; } = string.Empty;
}
