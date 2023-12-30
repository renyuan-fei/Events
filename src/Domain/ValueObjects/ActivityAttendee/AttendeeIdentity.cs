namespace Domain.ValueObjects.ActivityAttendee;

public class AttendeeIdentity : ValueObject
{
  public Guid UserId { get; private set; }
  public bool IsHost { get; private set; }

  protected override IEnumerable<object> GetEqualityComponents()
  {
    yield return UserId;
    yield return IsHost;
  }
}
