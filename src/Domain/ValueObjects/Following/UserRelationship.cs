namespace Domain.ValueObjects.Following;

public class UserRelationship : ValueObject
{
  public UserRelationship() { }

  public UserRelationship(UserId followerId, UserId followingId)
  {
    FollowerId = followerId;
    FollowingId = followingId;
  }

  public UserId FollowerId { get; }
  public UserId FollowingId { get; }

  protected override IEnumerable<object> GetEqualityComponents()
  {
    yield return FollowerId;
    yield return FollowingId;
  }
}
