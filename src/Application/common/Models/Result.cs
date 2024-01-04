namespace Application.common.Models;

/// <summary>
///   Represents the result of an operation.
/// </summary>
public class Result
{
  /// <summary>
  ///   Represents the result of an operation.
  /// </summary>
  private Result(bool succeeded, IEnumerable<string> errors)
  {
    Succeeded = succeeded;
    Errors = errors.ToArray();
  }

  public bool Succeeded { get; init; }

  /// <summary>
  ///   Property that represents a collection of errors.
  /// </summary>
  /// <remarks>
  ///   This property is used to store and access a collection of errors. It is a string array
  ///   that can be initialized during object construction using the 'init' keyword.
  ///   The Errors property is read-only after initialization.
  /// </remarks>
  public string[ ] Errors { get; init; }

  /// <summary>
  ///   Creates a success result.
  /// </summary>
  /// <returns>A success result.</returns>
  public static Result Success() { return new Result(true, Array.Empty<string>()); }

  /// <summary>
  ///   Create a Result instance indicating failure with specified errors.
  /// </summary>
  /// <param name="errors">The collection of error messages.</param>
  /// <returns>A Result instance indicating failure.</returns>
  public static Result Failure(IEnumerable<string> errors)
  {
    return new Result(false, errors);
  }
}
