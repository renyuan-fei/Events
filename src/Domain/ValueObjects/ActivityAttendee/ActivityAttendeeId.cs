namespace Domain.ValueObjects.ActivityAttendee;

public record ActivityAttendeeId(Guid Value)
{
  public static ActivityAttendeeId New(Guid value) => new(value);
}
