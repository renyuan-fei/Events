namespace Application.common.DTO;

/// <summary>
/// Represents a data transfer object for an activity attendee.
/// </summary>
public class ActivityAttendeeDTO
{
  public Guid Id { get; init; }

  /// <summary>
  /// Gets or sets a value indicating whether this instance is the host.
  /// </summary>
  /// <value>
  /// <c>true</c> if this instance is the host; otherwise, <c>false</c>.
  /// </value>
  public bool IsHost { get; set; }

  /// <summary>
  /// The display name of an object.
  /// </summary>
  /// <value>
  /// A string representing the display name.
  /// Can be null.
  /// </value>
  public string? DisplayName { get; set; }

  /// <summary>
  /// This property represents the user's username.
  /// </summary>
  /// <value>The username of the user.</value>
  public string? UserName { get; set; }

  /// <summary>
  /// Gets or sets the bio of a person.
  /// </summary>
  /// <remarks>
  /// The bio provides information about a person's background, experience, or other relevant details.
  /// </remarks>
  /// <value>
  /// A string representing the bio of a person.
  /// </value>
  public string? Bio { get; set; }
  //
  // /// <summary>
  // /// Gets or sets the image of an object.
  // /// </summary>
  // /// <value>
  // /// The image as a string.
  // /// </value>
  // public string? Image { get; set; }
}
