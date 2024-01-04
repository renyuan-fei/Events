namespace Domain.Events.Attendee;

public sealed class AttendeeAddedDomainEvent : BaseEvent
{
  public AttendeeAddedDomainEvent(
      Entities.Attendee attendee)
  {
    Attendee = attendee;
  }

  public Entities.Attendee Attendee { get; init; }
}
