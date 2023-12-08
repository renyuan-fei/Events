using Domain.Entities;

namespace Application.common.DTO;

/// <summary>
/// Represents an activity data transfer object.
/// </summary>
public class ActivityDTO
{
  /// <summary>
  /// Gets or sets the unique identifier for the property.
  /// </summary>
  /// <value>
  /// The unique identifier for the property.
  /// </value>
  public Guid Id { get; set; }

  /// <summary>
  /// Gets or sets the title of the property.
  /// </summary>
  /// <value>
  /// The title.
  /// </value>
  public string Title { get; set; }

  /// <summary>
  /// Gets or sets the date value.
  /// </summary>
  /// <value>
  /// The date value.
  /// </value>
  public DateTime Date { get; set; }

  /// <summary>
  /// Gets or sets the description of the property.
  /// </summary>
  /// <value>
  /// A string representing the property description.
  /// </value>
  public string Description { get; set; }

  /// <summary>
  /// Gets or sets the category of the property.
  /// </summary>
  /// <value>
  /// The category of the property.
  /// </value>
  public string Category { get; set; }

  /// <summary>
  /// Gets or sets the city.
  /// </summary>
  /// <value>
  /// The city.
  /// </value>
  public string City { get; set; }

  /// <summary>
  /// Gets or sets the venue of the event.
  /// </summary>
  /// <value>
  /// A string representing the venue of the event.
  /// </value>
  public string Venue { get; set; }

  public string HostUsername { get; set; }

  public bool   IsCancelled  { get; set; }

  /// <summary>
  /// Gets or sets the collection of attendees associated with the activity.
  /// </summary>
  /// <value>
  /// The collection of attendees associated with the activity.
  /// </value>
  public ICollection<ActivityAttendeeDTO> Attendees { get; set; }
}
