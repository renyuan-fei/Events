using Domain.Events.Following;
using Domain.ValueObjects.Following;

namespace Domain.Entities;

public class Follow : BaseAuditableEntity<FollowId>
{
  private Follow(FollowId id,UserRelationship relationship)
  {
    Id = id;
    Relationship = relationship;
  }

  private Follow() { }

  public UserRelationship Relationship { get; private set; }

  public static Follow Create(UserId followerId, UserId followingId)
  {
    var relationship = new UserRelationship(followerId, followingId);
    var following = new Follow(FollowId.New(),relationship);

    // 如果需要在创建时触发事件
    following.AddDomainEvent(new FollowedDomainEvent(following));

    return following;
  }

  public void Unfollow()
  {
    AddDomainEvent(new UnfollowedDomainEvent(Relationship.FollowerId, Relationship.FollowingId));
  }
}
