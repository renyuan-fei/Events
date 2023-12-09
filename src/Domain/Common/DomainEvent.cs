namespace Domain.Common;

/// <summary>
///   Represents an object that has domain events.
/// </summary>
public interface IHasDomainEvent
{
  /// <summary>
  ///   Represents a list of domain events.
  /// </summary>
  public List<DomainEvent> DomainEvents { get; set; }
}

/// <summary>
///   Base class for domain events.
/// </summary>
public abstract class DomainEvent
{
  /// <summary>
  ///   Represents a domain event in the system.
  /// </summary>
  protected DomainEvent() { DateOccurred = DateTimeOffset.UtcNow; }

  /// <summary>
  ///   Gets or sets a value indicating whether the object is published.
  /// </summary>
  /// <value>
  ///   <c>true</c> if the object is published; otherwise, <c>false</c>.
  /// </value>
  public bool IsPublished { get; set; }

  /// <summary>
  ///   Gets or sets the date and time when the event occurred.
  /// </summary>
  /// <value>
  ///   The date and time when the event occurred.
  /// </value>
  public DateTimeOffset DateOccurred { get; protected set; } = DateTime.UtcNow;
}
