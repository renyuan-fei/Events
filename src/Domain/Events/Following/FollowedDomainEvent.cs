namespace Domain.Events.Following;

public sealed class FollowedDomainEvent : BaseEvent
{
  public FollowedDomainEvent(Entities.Following following) { Following = following; }

  public Entities.Following Following { get; }
}
