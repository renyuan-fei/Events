namespace Application.common.interfaces;

/// <summary>
/// Represents an interface for retrieving the current date and time.
/// </summary>
public interface IDateTime
{
  /// <summary>
  /// Gets the current date and time.
  /// </summary>
  /// <value>
  /// The current date and time.
  /// </value>
  DateTime Now { get; }
}
