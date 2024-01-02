namespace Domain.Events.Following;

public sealed class UnfollowedDomainEvent : BaseEvent
{
  public UserId FollowerId  { get; private set; }
  public UserId FollowingId { get; private set; }

  public UnfollowedDomainEvent(UserId followerId, UserId followingId)
  {
    FollowerId = followerId;
    FollowingId = followingId;
  }
}
