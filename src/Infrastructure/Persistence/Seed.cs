using Domain.Entities;

using Infrastructure.DatabaseContext;
using Infrastructure.Identity;

using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistence;

public abstract class Seed
{
  public async static Task SeedData(
      ApplicationDbContext         context,
      UserManager<ApplicationUser> userManager)
  {
    if (context.Activities != null
     && !userManager.Users.Any()
     && !context.Activities.Any())
    {
      var users = new List<ApplicationUser>
      {
          new() { DisplayName = "Bob", UserName = "bob", Email = "bob@test.com" },
          new() { DisplayName = "Jane", UserName = "jane", Email = "jane@test.com" },
          new() { DisplayName = "Tom", UserName = "tom", Email = "tom@test.com" }
      };

      foreach (var user in users) { await userManager.CreateAsync(user, "Pa$$w0rd"); }

      var activities = new List<Activity>
      {
          new()
          {
              Title = "Past Activity 1",
              Date = DateTime.UtcNow.AddMonths(-2),
              Description = "Activity 2 months ago",
              Category = "drinks",
              City = "London",
              Venue = "Pub"
              // Attendees =
              //     new List<ActivityAttendee>
              //     {
              //         new ActivityAttendee { AppUser = users[0], IsHost = true }
              //     }
          },
          new()
          {
              Title = "Past Activity 2",
              Date = DateTime.UtcNow.AddMonths(-1),
              Description = "Activity 1 month ago",
              Category = "culture",
              City = "Paris",
              Venue = "The Louvre"
              // Attendees =
              //     new List<ActivityAttendee>
              //     {
              //         new ActivityAttendee { AppUser = users[0], IsHost = true },
              //         new ActivityAttendee { AppUser = users[1], IsHost = false },
              //     }
          },
          new()
          {
              Title = "Future Activity 1",
              Date = DateTime.UtcNow.AddMonths(1),
              Description = "Activity 1 month in future",
              Category = "music",
              City = "London",
              Venue = "Wembly Stadium"
              // Attendees =
              //     new List<ActivityAttendee>
              //     {
              //         new ActivityAttendee { AppUser = users[2], IsHost = true },
              //         new ActivityAttendee { AppUser = users[1], IsHost = false },
              //     }
          },
          new()
          {
              Title = "Future Activity 2",
              Date = DateTime.UtcNow.AddMonths(2),
              Description = "Activity 2 months in future",
              Category = "food",
              City = "London",
              Venue = "Jamies Italian"
              // Attendees =
              //     new List<ActivityAttendee>
              //     {
              //         new ActivityAttendee { AppUser = users[0], IsHost = true },
              //         new ActivityAttendee { AppUser = users[2], IsHost = false },
              //     }
          },
          new()
          {
              Title = "Future Activity 3",
              Date = DateTime.UtcNow.AddMonths(3),
              Description = "Activity 3 months in future",
              Category = "drinks",
              City = "London",
              Venue = "Pub"
              // Attendees =
              //     new List<ActivityAttendee>
              //     {
              //         new ActivityAttendee { AppUser = users[1], IsHost = true },
              //         new ActivityAttendee { AppUser = users[0], IsHost = false },
              //     }
          },
          new()
          {
              Title = "Future Activity 4",
              Date = DateTime.UtcNow.AddMonths(4),
              Description = "Activity 4 months in future",
              Category = "culture",
              City = "London",
              Venue = "British Museum"
              // Attendees =
              //     new List<ActivityAttendee>
              //     {
              //         new ActivityAttendee { AppUser = users[1], IsHost = true }
              //     }
          },
          new()
          {
              Title = "Future Activity 5",
              Date = DateTime.UtcNow.AddMonths(5),
              Description = "Activity 5 months in future",
              Category = "drinks",
              City = "London",
              Venue = "Punch and Judy"
              // Attendees =
              //     new List<ActivityAttendee>
              //     {
              //         new ActivityAttendee { AppUser = users[0], IsHost = true },
              //         new ActivityAttendee { AppUser = users[1], IsHost = false },
              //     }
          },
          new()
          {
              Title = "Future Activity 6",
              Date = DateTime.UtcNow.AddMonths(6),
              Description = "Activity 6 months in future",
              Category = "music",
              City = "London",
              Venue = "O2 Arena"
              // Attendees =
              //     new List<ActivityAttendee>
              //     {
              //         new ActivityAttendee { AppUser = users[2], IsHost = true },
              //         new ActivityAttendee { AppUser = users[1], IsHost = false },
              //     }
          },
          new()
          {
              Title = "Future Activity 7",
              Date = DateTime.UtcNow.AddMonths(7),
              Description = "Activity 7 months in future",
              Category = "travel",
              City = "Berlin",
              Venue = "All"
              // Attendees =
              //     new List<ActivityAttendee>
              //     {
              //         new ActivityAttendee { AppUser = users[0], IsHost = true },
              //         new ActivityAttendee { AppUser = users[2], IsHost = false },
              //     }
          },
          new()
          {
              Title = "Future Activity 8",
              Date = DateTime.UtcNow.AddMonths(8),
              Description = "Activity 8 months in future",
              Category = "drinks",
              City = "London",
              Venue = "Pub"
              // Attendees = new List<ActivityAttendee>
              // {
              //     new ActivityAttendee { AppUser = users[2], IsHost = true },
              //     new ActivityAttendee { AppUser = users[1], IsHost = false },
              // }
          }
      };

      await context.Activities.AddRangeAsync(activities);
      await context.SaveChangesAsync();
    }
  }
}
