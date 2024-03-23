namespace Domain.ValueObjects.Following;

public record FollowId(string Value)
{
  public static FollowId New() { return new FollowId(Guid.NewGuid().ToString()); }
}
