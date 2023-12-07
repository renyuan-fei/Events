using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

/// <summary>
/// Represents an attendee of an activity.
/// </summary>
public class ActivityAttendee : AuditableEntity
{
  /// <summary>
  /// Gets or sets the unique identifier for the property.
  /// </summary>
  [ Key ]
  public Guid Id { get; set; } = Guid.NewGuid();

  /// <summary>
  /// Gets or sets a value indicating whether the current instance is the host.
  /// </summary>
  /// <value>
  /// <c>true</c> if the current instance is the host; otherwise, <c>false</c>.
  /// </value>
  public bool IsHost { get; set; }

  /// <summary>
  /// The UserId property is used to store the unique identifier of the user associated with the activity attendee.
  /// </summary>
  public Guid UserId { get; set; }

  /// <summary>
  /// Gets or sets the display name.
  /// </summary>
  /// <value>
  /// The display name.
  /// </value>
  public string DisplayName { get; set; }

  /// <summary>
  /// Gets or sets the user name.
  /// </summary>
  /// <value>
  /// The user name.
  /// </value>
  public string UserName { get; set; }

  /// <summary>
  /// Gets or sets the bio of a person.
  /// </summary>
  /// <value>
  /// The bio of a person.
  /// </value>
  public string Bio { get; set; } = "Hello World!";

  /// <summary>
  /// Gets or sets the Activity property.
  /// </summary>
  /// <remarks>
  /// This property represents an activity.
  /// </remarks>
  public Activity Activity { get; set; }
}
