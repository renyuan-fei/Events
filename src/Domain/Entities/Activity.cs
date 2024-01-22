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
  private Activity(
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

  public string Title { get; private set; }

  public DateTime Date { get; private set; }

  public Category Category { get; private set; }

  public string Description { get; private set; }

  public Address Location { get; private set; }

  public ActivityStatus Status { get; private set; }

  public ICollection<Attendee> Attendees { get; set; } = new List<Attendee>();
  public ICollection<Comment>  Comments  { get; set; } = new List<Comment>();

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

    newActivity.AddDomainEvent(new ActivityCreatedDomainEvent(newActivity.Id));

    return newActivity;
  }

  public void Update(Activity updatedActivity)
  {
    Title = updatedActivity.Title;
    Date = updatedActivity.Date;
    Category = updatedActivity.Category;
    Description = updatedActivity.Description;
    Location = updatedActivity.Location;

    AddDomainEvent(new ActivityUpdatedDomainEvent(updatedActivity));
  }

  public void SetCategory(Category category)
  {
    AddDomainEvent(new ActivitySetCategoryDomainEvent(Id, category));
  }

  public void Cancel(DateTime utcNow)
  {
    var currentDate = DateOnly.FromDateTime(utcNow);

    if (currentDate > DateOnly.FromDateTime(Date)) { }

    Status = ActivityStatus.Canceled;

    AddDomainEvent(new ActivityCanceledDomainEvent(Id));
  }

  public void AddAttendee(Attendee attendee)
  {
    Attendees.Add(attendee);

    AddDomainEvent(new AttendeeAddedDomainEvent(attendee));
  }

  public void RemoveAttendee(UserId userId)
  {
    var attendee = Attendees.First(a => a.Identity.UserId == userId);

    Attendees.Remove(attendee);

    AddDomainEvent(new AttendeeRemovedDomainEvent(Id, userId));
  }

  public void AddComment(Comment comment)
  {
    Comments.Add(comment);

    AddDomainEvent(new CommentAddedDomainEvent(comment));
  }

  public void RemoveComment(CommentId commentId)
  {
    var comment = Comments.First(c => c.Id == commentId);

    Comments.Remove(comment);

    AddDomainEvent(new CommentRemovedDomainEvent(Id, commentId));
  }
}
