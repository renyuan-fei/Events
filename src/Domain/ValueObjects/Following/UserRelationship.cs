namespace Domain.ValueObjects.Following;

public class UserRelationship : ValueObject
{
  public UserRelationship() { }

  public UserRelationship(UserId followerId, UserId followeeId)
  {
    FollowerId = followerId;
    FolloweeId = followeeId;
  }

  public UserId FollowerId { get; }
  public UserId FolloweeId { get; }

  protected override IEnumerable<object> GetEqualityComponents()
  {
    yield return FollowerId;
    yield return FolloweeId;
  }
}
