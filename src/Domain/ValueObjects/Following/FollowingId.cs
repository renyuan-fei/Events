namespace Domain.ValueObjects.Following;

public record FollowingId(string Value)
{
  public static FollowingId New() => new FollowingId(Guid.NewGuid().ToString());
}
