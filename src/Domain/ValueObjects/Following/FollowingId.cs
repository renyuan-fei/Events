namespace Domain.ValueObjects.Following;

public record FollowingId(Guid Value)
{
  public static FollowingId New() => new FollowingId(Guid.NewGuid());
}
