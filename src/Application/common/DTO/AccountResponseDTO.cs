namespace Application.common.DTO;

/// <summary>
///   Represents the response data for an account request.
/// </summary>
public class AccountResponseDTO
{
  /// Gets or sets the display name.
  /// This property contains the display name, which is used to represent an entity or item in a user interface.
  /// The display name is a string value that can be assigned to the property. It can be null (if nullable string types are supported), or an empty string if no display name is provided
  /// .
  /// Usage:
  /// - To retrieve the stored display name value, access the property using the dot notation, e.g., `obj.DisplayName`.
  /// - To set a new display name, assign a string value to the property, e.g., `obj.DisplayName = "New Name"`.
  /// Example:
  /// var obj = new MyClass();
  /// obj.DisplayName = "John Doe";
  /// Remarks:
  /// - The property getter and setter are defined using auto-implemented properties, allowing convenient access and assignment without explicit field storage.
  /// - By default, the DisplayName property is initialized with an empty string to avoid null reference exceptions when accessing the property without assigning a value.
  /// - When using nullable string types, DisplayName can be set to null by assigning `null` or `default(string?)`.
  /// - It is recommended to provide a meaningful display name for better user experience and user interface context.
  /// @see MyClass
  /// @see DisplayAttribute
  /// /
  public string? DisplayName { get; set; } = string.Empty;

  /// <summary>
  ///   Gets or sets the email address.
  /// </summary>
  /// <value>
  ///   The email address. This property can be null.
  /// </value>
  public string? Email { get; set; } = string.Empty;

  /// <summary>
  ///   Gets or sets the token.
  /// </summary>
  public string? Token { get; set; } = string.Empty;

  /// <summary>
  ///   Gets or sets the expiration date and time.
  /// </summary>
  /// <value>
  ///   The expiration date and time.
  /// </value>
  public DateTime ExpirationDateTime { get; set; } = DateTime.MinValue;
}
