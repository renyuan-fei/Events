using System.Reflection;

using Application.common.Interfaces;
using Application.Common.Interfaces;

using Domain.Entities;

using Infrastructure.Identity;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DatabaseContext;

public class EventsDbContext : IdentityDbContext<ApplicationUser, ApplicationRole,
    string>, IEventsDbContext, IUnitOfWork, IAppIdentityDbContext
{
  public EventsDbContext(DbContextOptions<EventsDbContext> options) : base(options) { }

  public DbSet<Activity>         Activities        { get; set; }
  public DbSet<Attendee>         Attendees         { get; set; }
  public DbSet<Photo>            Photos            { get; set; }
  public DbSet<Follow>           Following         { get; set; }
  public DbSet<Comment>          Comments          { get; set; }
  public DbSet<UserNotification> UserNotifications { get; set; }
  public DbSet<Notification>     Notifications     { get; set; }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    base.OnModelCreating(builder);
  }
}
