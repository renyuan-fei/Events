namespace Domain.Common;

// Learn more: https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/microservice-ddd-cqrs-patterns/implement-value-objects
/// <summary>
///   Base class for value objects.
/// </summary>
public abstract class ValueObject
{
  /// <summary>
  ///   Determines whether two ValueObjects are equal.
  /// </summary>
  /// <param name="left">The left ValueObject to compare.</param>
  /// <param name="right">The right ValueObject to compare.</param>
  /// <returns>
  ///   <c>true</c> if the instances are equal; otherwise, <c>false</c>.
  /// </returns>
  protected static bool EqualOperator(ValueObject left, ValueObject right)
  {
    if (left is null ^ right is null) { return false; }

    return left?.Equals(right!) != false;
  }

  /// <summary>
  ///   Checks if two ValueObject instances are not equal.
  /// </summary>
  /// <param name="left">The first ValueObject instance.</param>
  /// <param name="right">The second ValueObject instance.</param>
  /// <returns>
  ///   Returns true if the two ValueObject instances are not equal, otherwise returns
  ///   false.
  /// </returns>
  protected static bool NotEqualOperator(ValueObject left, ValueObject right)
  {
    return !EqualOperator(left, right);
  }

  /// <summary>
  ///   Gets the equality components that define the equality for the current object.
  /// </summary>
  /// <returns>
  ///   An <see cref="IEnumerable{T}" /> of <see cref="object" /> representing the equality
  ///   components.
  /// </returns>
  protected abstract IEnumerable<object> GetEqualityComponents();

  /// <summary>
  ///   Determines whether the current instance is equal to the specified object.
  /// </summary>
  /// <param name="obj">The object to compare with the current instance.</param>
  /// <returns>True if the current instance is equal to the specified object; otherwise, false.</returns>
  public override bool Equals(object? obj)
  {
    if (obj == null
     || obj.GetType() != GetType()) { return false; }

    var other = (ValueObject)obj;

    return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
  }

  /// <summary>
  ///   Returns the hash code for this object.
  /// </summary>
  /// <remarks>
  ///   The hash code is calculated by combining the hash codes of all the equality components
  ///   using the XOR (^) operation.
  /// </remarks>
  /// <returns>
  ///   An <see cref="int" /> representing the hash code for this object.
  /// </returns>
  public override int GetHashCode()
  {
    return GetEqualityComponents()
           .Select(x => x != null
                       ? x.GetHashCode()
                       : 0)
           .Aggregate((x, y) => x ^ y);
  }
}
