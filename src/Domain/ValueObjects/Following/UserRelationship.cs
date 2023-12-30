namespace Domain.ValueObjects.Following;

public class UserRelationship : ValueObject
{
  public UserRelationship() {
  }

  public UserRelationship(UserId followerId, UserId followeeId)
  {
    FollowerId = followerId;
    FolloweeId = followeeId;
  }

  public UserId FollowerId { get; private set; }
  public UserId FolloweeId { get; private set; }

  protected override IEnumerable<object> GetEqualityComponents()
  {
    yield return FollowerId;
    yield return FolloweeId;
  }
}
