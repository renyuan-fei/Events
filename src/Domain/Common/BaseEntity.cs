using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common;

public abstract class BaseEntity <TEntityId>
{
  protected BaseEntity(TEntityId id)
  {
    Id = id;
  }

  protected BaseEntity()
  {
  }
  public TEntityId Id { get; set; }

  // public int Id { get; set; }

  private readonly List<BaseEvent> _domainEvents = new();

  [ NotMapped ]
  public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

  public void AddDomainEvent(BaseEvent domainEvent)
  {
    _domainEvents.Add(domainEvent);
  }

  public void RemoveDomainEvent(BaseEvent domainEvent)
  {
    _domainEvents.Remove(domainEvent);
  }

  public void ClearDomainEvents()
  {
    _domainEvents.Clear();
  }
}
