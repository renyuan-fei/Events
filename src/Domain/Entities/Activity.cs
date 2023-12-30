using System.ComponentModel.DataAnnotations;

using Domain.Enums;
using Domain.Events.Activity;
using Domain.ValueObjects.Activity;

namespace Domain.Entities;

public class Activity : BaseAuditableEntity<ActivityId>
{
  private Activity(ActivityId id, string title, DateTime date, Category category,
                  string     description,
                  Address    location,
                  bool       isCancelled)
  {
    Id = id;
    Title = title;
    Date = date;
    Category = category;
    Description = description;
    Location = location;
    IsCancelled = isCancelled;
  }

  private Activity() {

  }

  public string Title { get; private set; }

  public DateTime Date { get; private set; }

  public Category Category { get; private set; }

  public string Description { get; private set; }

  public Address Location { get; private set; }

  public bool IsCancelled { get; private set; }

  public ICollection<Attendee> Attendees { get; set; } = new List<Attendee>();
  public ICollection<Comment> Comments { get; set; } = new List<Comment>();



  public static Activity Create(
      string     title,
      DateTime   date,
      Category   category,
      string     description,
      Address    address)
  {
    var newActivity =  new Activity(ActivityId.New(), title, date, category, description, address,
        false);

    newActivity.AddDomainEvent(new CreateActivityDomainEvent(newActivity.Id));

    return newActivity;
  }

  public Activity Update(Activity updatedActivity)
  {
    throw new NotImplementedException();
  }

  public Activity SetCategory(Category category)
  {
    throw new NotImplementedException();
  }

  public Activity Cancel(ActivityId id)
  {
    throw new NotImplementedException();
  }

  public Activity AddAttendee(ActivityId id, Attendee attendee)
  {
    throw new NotImplementedException();
  }

  public Activity RemoveAttendee(ActivityId id, Attendee attendee)
  {
    throw new NotImplementedException();
  }

  public Activity AddComment(ActivityId id, Comment comment)
  {
    throw new NotImplementedException();
  }

  public Activity RemoveComment(ActivityId id, Comment comment)
  {
    throw new NotImplementedException();
  }
}
