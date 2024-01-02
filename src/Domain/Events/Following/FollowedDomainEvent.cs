namespace Domain.Events.Following;

public sealed class FollowedDomainEvent : BaseEvent
{
  public Entities.Following Following { get; }

  public FollowedDomainEvent(Entities.Following following) { Following = following; }
}
