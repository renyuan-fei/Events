using Application.common.interfaces;
using Application.Common.Interfaces;

using Domain.Entities;

using Duende.IdentityServer.EntityFramework.Options;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure.DatabaseContext;

public class EventsDbContext : DbContext, IEventsDbContext
{
  private readonly ICurrentUserService _currentUserService;

  private readonly IDateTime _dateTime;

  private readonly IDomainEventService _domainEventService;

  public EventsDbContext(DbContextOptions<EventsDbContext> options) :
      base(options)
  {
  }

  public EventsDbContext(
      DbContextOptions<EventsDbContext> options,
      IOptions<OperationalStoreOptions> operationalStoreOptions,
      ICurrentUserService               currentUserService,
      IDateTime                         dateTime,
      IDomainEventService               domainEventService) :
      base(options)
  {
    _currentUserService = currentUserService;
    _dateTime = dateTime;
    _domainEventService = domainEventService;
  }

  public EventsDbContext(
      ICurrentUserService currentUserService,
      IDateTime           dateTime,
      IDomainEventService domainEventService)
  {
    _currentUserService = currentUserService;
    _dateTime = dateTime;
    _domainEventService = domainEventService;
  }

  public DbSet<Activity> Activities => Set<Activity>();

  public DbSet<ActivityAttendee> ActivityAttendees => Set<ActivityAttendee>();

  public DbSet<Photo>     Photos     => Set<Photo>();

  public DbSet<Following> Followings => Set<Following>();

  public DbSet<Comment> Comments => Set<Comment>();
}
