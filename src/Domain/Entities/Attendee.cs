using Domain.ValueObjects.Activity;
using Domain.ValueObjects.ActivityAttendee;
using Domain.ValueObjects.Attendee;

namespace Domain.Entities;

public class Attendee : BaseAuditableEntity<AttendeeId>
{
  private Attendee(
      AttendeeId       id,
      AttendeeIdentity identity,
      ActivityId       activityId,
      Activity         activity)
  {
    Id = id;
    Identity = identity;
    ActivityId = activityId;
    Activity = activity;
  }

  private Attendee() { }

  public AttendeeIdentity Identity { get; private set; }

  public ActivityId ActivityId { get; private set; }
  public Activity   Activity   { get; private set; }

  public static Attendee Create(
      UserId     userId,
      bool       isHost,
      ActivityId activityId,
      Activity   activity)
  {
    var attendeeIdentity = AttendeeIdentity.Create(userId, isHost);

    return new Attendee(AttendeeId.New(), attendeeIdentity, activityId, activity);
  }
}
