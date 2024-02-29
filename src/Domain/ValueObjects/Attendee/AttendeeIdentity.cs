namespace Domain.ValueObjects.ActivityAttendee;

public class AttendeeIdentity : ValueObject
{
  private AttendeeIdentity() { }

  private AttendeeIdentity(UserId userId, bool isHost)
  {
    UserId = userId;
    IsHost = isHost;
  }

  public UserId UserId { get; }
  public bool   IsHost { get; }

  public static AttendeeIdentity Create(UserId userId, bool isHost)
  {
    return new AttendeeIdentity(userId, isHost);
  }

  protected override IEnumerable<object> GetEqualityComponents()
  {
    yield return UserId;
    yield return IsHost;
  }
}
