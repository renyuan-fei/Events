using Domain.Events.Following;
using Domain.ValueObjects.Following;

namespace Domain.Entities;

public class Following : BaseAuditableEntity<FollowingId>
{
  private Following(UserRelationship relationship) { Relationship = relationship; }

  private Following() { }

  public UserRelationship Relationship { get; private set; }

  private Following Follow(UserId followerId, UserId followingId)
  {
    var relationship = new UserRelationship(followerId, followingId);

    return new Following(relationship);
  }

  public void Followed(UserId followerId, UserId followingId)
  {
    var newFollowing = Follow(followerId, followingId);

    AddDomainEvent(new FollowedDomainEvent(newFollowing));
  }

  public void Unfollow(UserId followerId, UserId followingId)
  {
    AddDomainEvent(new UnfollowedDomainEvent(followerId, followingId));
  }
}
