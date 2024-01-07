using Application.common.DTO.Interface;

namespace Application.common.DTO;

/// <summary>
///   The data transfer object representing a user's following.
/// </summary>
public class FollowerDTO : IUserDetail
{
  /// <summary>
  ///   Gets or sets the unique identifier of the user.
  /// </summary>
  /// <value>
  ///   The user identifier.
  /// </value>
  public string UserId { get; init; }

  /// <summary>
  ///   Gets or sets the display name.
  /// </summary>
  /// <value>
  ///   The display name of the object.
  /// </value>
  public string? DisplayName { get; set; }

  /// <summary>
  ///   Gets or sets the username associated with the property.
  /// </summary>
  /// <value>The username as a string value, or null if not specified.</value>
  public string? UserName { get; set; }

  public string? Bio { get; set; }

  /// <summary>
  ///   Gets or sets the image.
  /// </summary>
  /// <remarks>
  ///   The image should be a string representing the image path or URL.
  ///   It can be null if no image is provided.
  /// </remarks>
  public string? Image { get; set; }


}
