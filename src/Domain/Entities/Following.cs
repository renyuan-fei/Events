using System.ComponentModel.DataAnnotations;

using Domain.ValueObjects;
using Domain.ValueObjects.Following;

namespace Domain.Entities;

public class Following : BaseAuditableEntity<FollowingId>
{
  private Following(UserRelationship relationship) { Relationship = relationship; }

  private Following() { }

  public UserRelationship Relationship { get; private set; }

  public static Following Follow(UserId followerId, UserId followingId)
  {
    var relationship = new UserRelationship(followerId, followingId);

    return new Following(relationship);
  }

  public static Following Unfollow(UserId followerId, UserId followingId)
  {
    throw new NotImplementedException();
  }
}
