using Domain.Common;

namespace Application.common.interfaces;

public interface IDomainEventService
{
  Task Publish(DomainEvent domainEvent);
}
