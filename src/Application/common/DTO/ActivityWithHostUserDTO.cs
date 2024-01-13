namespace Application.common.DTO;

/// <summary>
/// DTO for activity with information about the host user.
/// </summary>
public class ActivityWithHostUserDTO
{
  /// <summary>
  /// Identifier for the activity.
  /// </summary>
  public string Id { get; set; }

  /// <summary>
  /// Title of the activity.
  /// </summary>
  public string Title { get; set; }

  /// <summary>
  /// URL of the activity's main image.
  /// </summary>
  public string? ImageUrl { get; set; }

  /// <summary>
  /// Date and time when the activity takes place.
  /// </summary>
  public DateTime Date { get; set; }

  /// <summary>
  /// Category of the activity.
  /// </summary>
  public string Category { get; set; }

  /// <summary>
  /// City where the activity is located.
  /// </summary>
  public string City { get; set; }

  /// <summary>
  /// Specific venue of the activity within the city.
  /// </summary>
  public string Venue { get; set; }

  public int goingCount { get; set; }

  /// <summary>
  /// Host user details for the activity.
  /// </summary>
  public HostUserDTO HostUser { get; set; }
}

/// <summary>
/// DTO for host user details.
/// </summary>
public class HostUserDTO
{
  /// <summary>
  /// Username of the host.
  /// </summary>
  public string Username { get; set; }


  // public string ImageUrl { get; set; }

  /// <summary>
  /// Identifier for the host user.
  /// </summary>
  public string Id { get; set; }
}
