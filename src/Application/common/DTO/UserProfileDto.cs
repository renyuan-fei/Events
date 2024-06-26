namespace Application.common.DTO;

/// <summary>
///   Represents a data transfer object for user information.
/// </summary>
public class UserProfileDto
{
  /// <summary>
  ///   Gets or sets the unique identifier for the property.
  /// </summary>
  /// <value>
  ///   The unique identifier.
  /// </value>
  public string Id { get; set; }

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

  public string Image { get; set; }

  public int Followers { get; set; }

  public int Following { get; set; }

  // public List<PhotoDTO> Photos { get; set; }

  // public List<ActivityWithHostUserDTO> Activities { get; set; }
}
