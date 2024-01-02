using Domain.ValueObjects.Activity;

namespace Domain.Events.Attendee;

public sealed class AttendeeAddedDomainEvent : BaseEvent
{
  public Entities.Attendee Attendee   { get; init; }

  public AttendeeAddedDomainEvent(
      Entities.Attendee attendee)
  {
    Attendee = attendee;
  }
}
