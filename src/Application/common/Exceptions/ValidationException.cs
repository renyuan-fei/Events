using FluentValidation.Results;

namespace Application.common.Exceptions;

/// <summary>
///   Represents an exception that is thrown when one or more validation failures have
///   occurred.
/// </summary>
public class ValidationException : Exception
{
  /// <summary>
  ///   Represents a validation exception that is thrown when one or more validation failures
  ///   have occurred.
  /// </summary>
  /// <remarks>
  ///   This class represents an exception that is thrown when there are one or more validation
  ///   failures during the validation process.
  ///   The ValidationException is a subclass of the base Exception class and it contains a
  ///   dictionary of validation errors.
  /// </remarks>
  public ValidationException()
      : base("One or more validation failures have occurred.")
  {
    Errors = new Dictionary<string, string[ ]>();
  }

  /// <summary>
  ///   Represents an exception that is thrown when there are validation failures.
  /// </summary>
  public ValidationException(IEnumerable<ValidationFailure> failures)
      : this()
  {
    Errors = failures
             .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
             .ToDictionary(failureGroup => failureGroup.Key,
                           failureGroup => failureGroup.ToArray());
  }

  /// <summary>
  ///   Gets a dictionary of errors associated with the given property.
  /// </summary>
  /// <remarks>
  ///   The dictionary key represents the property name, and the value is an array of strings
  ///   representing the associated errors.
  /// </remarks>
  /// <returns>
  ///   A dictionary where each key is a property name, and the corresponding value is an array
  ///   of strings representing the associated errors.
  /// </returns>
  public IDictionary<string, string[ ]> Errors { get; }
}
