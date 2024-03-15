namespace Domain.Events.Attendee;

public sealed class AttendeeAddedDomainEvent : BaseEvent
{
  public AttendeeAddedDomainEvent(Entities.Attendee attendee,Entities.Activity activity)
  {
    Attendee = attendee;
    Activity = activity;
  }

  public Entities.Activity Activity { get; init; }
  public Entities.Attendee Attendee { get; init; }
}
