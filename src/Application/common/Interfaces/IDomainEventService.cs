using Domain.Common;

namespace Application.common.interfaces;

/// <summary>
///   Represents a domain event service used for publishing domain events.
/// </summary>
public interface IDomainEventService
{
  /// <summary>
  ///   Publishes the given domain event asynchronously.
  /// </summary>
  /// <param name="domainEvent">The domain event to be published.</param>
  /// <returns>A task representing the asynchronous publication of the domain event.</returns>
  Task Publish(DomainEvent domainEvent);
}
