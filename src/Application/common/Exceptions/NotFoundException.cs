namespace Application.common.Exceptions;

/// <summary>
///   Represents an exception that is thrown when an entity is not found.
/// </summary>
public class NotFoundException : Exception
{
  /// <summary>
  ///   Initializes a new instance of the NotFoundException class.
  /// </summary>
  public NotFoundException() { }

  /// <summary>
  ///   Represents an exception that is thrown when a specific item is not found.
  /// </summary>
  /// <seealso cref="System.Exception" />
  public NotFoundException(string message)
      : base(message)
  {
  }

  /// <summary>
  ///   Initializes a new instance of the <see cref="NotFoundException" /> class with the
  ///   specified error message and inner exception.
  /// </summary>
  /// <param name="message">The error message that explains the reason for the exception.</param>
  /// <param name="innerException">
  ///   The exception that is the cause of the current exception, or
  ///   a null reference if no inner exception is specified.
  /// </param>
  public NotFoundException(string message, Exception innerException)
      : base(message, innerException)
  {
  }

  /// <summary>
  ///   Represents an exception that is thrown when an entity is not found.
  /// </summary>
  /// <remarks>
  ///   This exception is typically thrown when an entity with a specific name and key is not
  ///   found.
  /// </remarks>
  public NotFoundException(string name, object key)
      : base($"Entity \"{name}\" ({key}) was not found.")
  {
  }
}
