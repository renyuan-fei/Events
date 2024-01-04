namespace Domain.ValueObjects.ActivityAttendee;

public record AttendeeId(string Value)
{
  public static AttendeeId New() { return new AttendeeId(Guid.NewGuid().ToString()); }
}
