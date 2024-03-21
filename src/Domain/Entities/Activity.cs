using Domain.Events.Activity;
using Domain.Events.Attendee;
using Domain.Events.Comment;
using Domain.ValueObjects.Activity;
using Domain.ValueObjects.ActivityAttendee;
using Domain.ValueObjects.Comment;
using Domain.ValueObjects.Photo;

namespace Domain.Entities;

public class Activity : BaseAuditableEntity<ActivityId>
{
  public string Title { get; private set; }

  public DateTime Date { get; private set; }

  public Category Category { get; private set; }

  public string Description { get; private set; }

  public Address Location { get; private set; }

  public ActivityStatus Status { get; private set; }

  private readonly List<Attendee> _attendees = new List<Attendee>();
  private readonly List<Comment>  _comments  = new List<Comment>();

  public IReadOnlyCollection<Attendee> Attendees => _attendees.AsReadOnly();
  public IReadOnlyCollection<Comment>  Comments  => _comments.AsReadOnly();

  public Activity(
      ActivityId     id,
      string         title,
      DateTime       date,
      Category       category,
      string         description,
      Address        location,
      ActivityStatus activityStatus)
  {
    Id = id;
    Title = title;
    Date = date;
    Category = category;
    Description = description;
    Location = location;
    Status = activityStatus;
  }

  private Activity() { }

  public static Activity Create(
      string   title,
      DateTime date,
      Category category,
      string   description,
      Address  address)
  {
    var newActivity = new Activity(ActivityId.New(),
                                   title,
                                   date,
                                   category,
                                   description,
                                   address,
                                   ActivityStatus.Confirmed);

    newActivity.AddDomainEvent(new ActivityCreatedDomainEvent(newActivity));

    return newActivity;
  }

  public void Update(Activity updatedActivity)
  {
    Title = updatedActivity.Title;
    Date = updatedActivity.Date;
    Category = updatedActivity.Category;
    Description = updatedActivity.Description;
    Location = updatedActivity.Location;

    AddDomainEvent(new ActivityUpdatedDomainEvent(Id, Title));
  }

  public void Cancel()
  {
    Status = ActivityStatus.Canceled;

    AddDomainEvent(new ActivityCanceledDomainEvent(Id, Title));
  }

  public void Reactive()
  {
    Status = ActivityStatus.Confirmed;

    AddDomainEvent(new ActivityCanceledDomainEvent(Id, Title));
  }

  public void AddAttendee(Attendee attendee)
  {
    _attendees.Add(attendee);

    AddDomainEvent(new AttendeeAddedDomainEvent(attendee,this));
  }

  public bool RemoveAttendee(UserId userId)
  {
    var attendee = _attendees.SingleOrDefault(a => a.Identity.UserId == userId);

    if (attendee == null) return false;

    _attendees.Remove(attendee);
    AddDomainEvent(new AttendeeRemovedDomainEvent(Id, userId));

    return true;
  }

  public void AddComment(Comment comment)
  {
    _comments.Add(comment);
    AddDomainEvent(new CommentAddedDomainEvent(comment));
  }

  public bool RemoveComment(CommentId commentId)
  {
    var comment = _comments.SingleOrDefault(c => c.Id == commentId);

    if (comment == null) return false;

    _comments.Remove(comment);
    AddDomainEvent(new CommentRemovedDomainEvent(Id, commentId));

    return true;
  }
}
