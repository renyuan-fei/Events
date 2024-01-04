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

  public DbSet<Attendee> ActivityAttendees => Set<Attendee>();

  public DbSet<Photo> Photos => Set<Photo>();

  public DbSet<Following> Followings => Set<Following>();

  public DbSet<Comment> Comments => Set<Comment>();

  protected override void OnModelCreating(ModelBuilder builder)
  {
    builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    base.OnModelCreating(builder);
  }
}
