namespace Domain.ValueObjects.Following;

public class UserRelationship : ValueObject
{
  public Guid FollowerId { get; private set; }
  public Guid FolloweeId { get; private set; }

  // ... 构造函数和方法 ...
  protected override IEnumerable<object> GetEqualityComponents() { throw new NotImplementedException(); }
}
