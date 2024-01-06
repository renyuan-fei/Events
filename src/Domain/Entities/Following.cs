using Domain.Events.Following;
using Domain.ValueObjects.Following;

namespace Domain.Entities;

public class Following : BaseAuditableEntity<FollowingId>
{
  private Following(FollowingId id,UserRelationship relationship)
  {
    Id = id;
    Relationship = relationship;
  }

  private Following() { }

  public UserRelationship Relationship { get; private set; }

  public static Following Create(UserId followerId, UserId followingId)
  {
    var relationship = new UserRelationship(followerId, followingId);
    var following = new Following(FollowingId.New(),relationship);

    // 如果需要在创建时触发事件
    following.AddDomainEvent(new FollowedDomainEvent(following));

    return following;
  }

  public void Unfollow()
  {
    AddDomainEvent(new UnfollowedDomainEvent(Relationship.FollowerId, Relationship.FollowingId));
  }
}
