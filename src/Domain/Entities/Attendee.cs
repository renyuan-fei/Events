using Domain.ValueObjects;
using Domain.ValueObjects.Activity;
using Domain.ValueObjects.ActivityAttendee;

namespace Domain.Entities;

public class Attendee : BaseAuditableEntity<ActivityAttendeeId>
{
  private Attendee(AttendeeIdentity identity, ActivityId activityId, Activity activity)
  {
    Identity = identity;
    ActivityId = activityId;
    Activity = activity;
  }

  private Attendee() {
  }

  public AttendeeIdentity   Identity { get; private set; }

  public ActivityId ActivityId { get; private set; }
  public Activity   Activity   { get; private set; }

  public Attendee AddAttendee(ActivityId id, Attendee attendee)
  {
    throw new NotImplementedException();
  }

  public Attendee RemoveAttendee(ActivityId id, Attendee attendee)
  {
    throw new NotImplementedException();
  }
}
