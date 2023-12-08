using Domain.Common;

using MediatR;

namespace Application.common.Models;

/// <summary>
///   Represents a domain event notification.
/// </summary>
/// <typeparam name="TDomainEvent">The type of domain event.</typeparam>
public class DomainEventNotification <TDomainEvent> : INotification
where TDomainEvent : DomainEvent
{
  public DomainEventNotification(TDomainEvent domainEvent) { DomainEvent = domainEvent; }

  public TDomainEvent DomainEvent { get; }
}
