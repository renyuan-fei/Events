namespace Domain.Events.Following;

public sealed class FollowedDomainEvent : BaseEvent
{
  public FollowedDomainEvent(Entities.Follow follow) { Follow = follow; }

  public Entities.Follow Follow { get; }
}
