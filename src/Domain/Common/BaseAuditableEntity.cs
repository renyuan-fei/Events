using Domain.Common.Contracts;

namespace Domain.Common;

/// <summary>
///   Represents an auditable entity in the system.
/// </summary>
public abstract class BaseAuditableEntity<TEntityId> : BaseEntity<TEntityId>, IBaseAuditableEntity
{
  /// <summary>
  ///   Gets or sets the date and time when the property was created.
  /// </summary>
  /// <value>
  ///   The date and time when the property was created.
  /// </value>
  public DateTimeOffset Created { get; set; }

  /// <summary>
  ///   Gets or sets the unique identifier of the user who created this object.
  /// </summary>
  /// <value>
  ///   A <see cref="System.Guid" /> representing the unique identifier of the user
  ///   who created this object. Returns null if no user is associated with the creation.
  /// </value>
  public string? CreatedBy { get; set; }

  /// <summary>
  ///   Gets or sets the last modified date and time.
  /// </summary>
  /// <value>
  ///   The last modified date and time.
  /// </value>
  public DateTimeOffset LastModified { get; set; }

  /// <summary>
  ///   Gets or sets the last user who modified the property.
  /// </summary>
  /// <value>
  ///   The last user who modified the property. Returns null if no user has modified it.
  /// </value>
  public string? LastModifiedBy { get; set; }
}
