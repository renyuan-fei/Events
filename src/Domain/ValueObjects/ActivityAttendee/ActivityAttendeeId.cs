namespace Domain.ValueObjects.ActivityAttendee;

public record ActivityAttendeeId(string Value)
{
  public static ActivityAttendeeId New() => new(Guid.NewGuid().ToString());
}
