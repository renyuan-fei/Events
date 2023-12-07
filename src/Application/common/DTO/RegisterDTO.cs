using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

namespace Application.common.DTO;

/// <summary>
/// Represents a data transfer object for registering a user.
/// </summary>
public class RegisterDTO
{
  /// <summary>
  /// The display name property.
  /// </summary>
  /// <remarks>
  /// This property is used to store the display name value.
  /// </remarks>
  /// <value>
  /// The display name value.
  /// </value>
  /// <exception cref="System.ComponentModel.DataAnnotations.RequiredAttribute">
  /// Thrown when the value is not provided.
  /// </exception>
  /// <example>
  /// This example shows how to use the DisplayName property:
  /// <code>
  /// var obj = new MyClass();
  /// obj.DisplayName = "John Doe";
  /// </code>
  /// </example>
  [ Required(ErrorMessage = "Display name is required") ]
  public string DisplayName { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the Email property.
  /// </summary>
  /// <value>
  /// The Email property.
  /// </value>
  /// <remarks>
  /// The Email property is used to store and retrieve the email address of a user.
  /// </remarks>
  [ Required(ErrorMessage = "Email is required") ]
  [ EmailAddress(ErrorMessage = "Email is not valid") ]
  [ Remote("IsEmailAlreadyRegistered",
           "Account",
           ErrorMessage = "Email is already is use") ]
  public string Email { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the phone number.
  /// </summary>
  /// <value>
  /// The phone number.
  /// </value>
  /// <remarks>
  /// This property represents the phone number of a user.
  /// It is required and should contain only digits.
  /// Additionally, it is validated remotely to check if it is already in use.
  /// </remarks>
  [ Required(ErrorMessage = "Phone number is required") ]
  [ RegularExpression("^[0-9]*$",
                      ErrorMessage = "Phone number should contain digits only") ]
  [ Remote("IsEmailAlreadyRegister",
           "Account",
           ErrorMessage = "Email is already is use") ]
  public string? PhoneNumber { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the password.
  /// </summary>
  [ Required(ErrorMessage = "Password is required") ]
  public string Password { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the confirm password.
  /// </summary>
  /// <value>
  /// The confirm password.
  /// </value>
  [ Required(ErrorMessage = "Confirm password is required") ]
  [ Compare("Password", ErrorMessage = "Password and confirm password do not match") ]
  public string ConfirmPassword { get; set; } = string.Empty;
}
