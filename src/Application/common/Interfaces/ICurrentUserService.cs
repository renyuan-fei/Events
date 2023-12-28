namespace Application.common.interfaces;

/// <summary>
///   Represents a service for retrieving the current user's ID.
/// </summary>
public interface ICurrentUserService
{
  /// <summary>
  ///   Gets the unique identifier of the user.
  /// </summary>
  /// <remarks>
  ///   The User ID property represents the unique identifier of the user.
  /// </remarks>
  /// <value>
  ///   The unique identifier of the user.
  /// </value>
  /// <remarks>
  ///   This property is of type <see cref="System.Guid" /> and can also be null.
  /// </remarks>
  string? Id { get; }
}
