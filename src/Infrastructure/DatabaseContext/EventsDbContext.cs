using System.Reflection;

using Application.common.Interfaces;
using Application.Common.Interfaces;

using Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DatabaseContext;

public class EventsDbContext : DbContext, IEventsDbContext, IUnitOfWork
{
  public EventsDbContext(DbContextOptions<EventsDbContext> options) : base(options) { }

  public DbSet<Activity> Activities => Set<Activity>();

  public DbSet<Attendee> Attendees => Set<Attendee>();

  public DbSet<Photo> Photos => Set<Photo>();

  public DbSet<Following> Following => Set<Following>();

  public DbSet<Comment> Comments => Set<Comment>();

  public DbSet<UserNotification> UserNotifications => Set<UserNotification>();

  public DbSet<Notification> Notifications => Set<Notification>();

  protected override void OnModelCreating(ModelBuilder builder)
  {
    builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    base.OnModelCreating(builder);
  }
}
