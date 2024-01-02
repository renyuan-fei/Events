namespace Domain.ValueObjects.ActivityAttendee;

public class AttendeeIdentity : ValueObject
{
  private AttendeeIdentity() { }

  private AttendeeIdentity(UserId userId, bool isHost)
  {
    UserId = userId;
    IsHost = isHost;
  }

  public static AttendeeIdentity Create(UserId userId, bool isHost)
  {
    return new AttendeeIdentity(userId, isHost);
  }

  public UserId UserId { get; private set; }
  public bool   IsHost { get; private set; }

  protected override IEnumerable<object> GetEqualityComponents()
  {
    yield return UserId;
    yield return IsHost;
  }
}
