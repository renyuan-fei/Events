namespace Domain.ValueObjects.ActivityAttendee;

public record AttendeeId(string Value)
{
  public static AttendeeId New() => new(Guid.NewGuid().ToString());
}
