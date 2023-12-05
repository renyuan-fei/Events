using Application.common.interfaces;
using Application.Common.Interfaces;

using CleanArchitecture.Application.Common.Interfaces;

using Domain.Common;
using Domain.Entities;

using Duende.IdentityServer.EntityFramework.Options;

using Infrastructure.Identity;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace Infrastructure.DatabaseContext;

public class ApplicationDbContext :
    IdentityDbContext<ApplicationUser, ApplicationRole, Guid>,
    IApplicationDbContext
{
  private readonly ICurrentUserService _currentUserService;
  private readonly IDateTime           _dateTime;
  private readonly IDomainEventService _domainEventService;

  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
      base(options)
  {
  }

  public ApplicationDbContext(
      DbContextOptions<ApplicationDbContext> options,
      IOptions<OperationalStoreOptions>      operationalStoreOptions,
      ICurrentUserService                    currentUserService,
      IDateTime                              dateTime,
      IDomainEventService                    domainEventService) :
      base(options)
  {
    _currentUserService = currentUserService;
    _dateTime = dateTime;
    _domainEventService = domainEventService;
  }

  public ApplicationDbContext(
      ICurrentUserService currentUserService,
      IDateTime           dateTime,
      IDomainEventService domainEventService)
  {
    _currentUserService = currentUserService;
    _dateTime = dateTime;
    _domainEventService = domainEventService;
  }

  public DbSet<Activity>         Activities        => Set<Activity>();
  public DbSet<ActivityAttendee> ActivityAttendees => Set<ActivityAttendee>();

  public async override Task<int> SaveChangesAsync(
      CancellationToken cancellationToken = new())
  {
    foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
    {
      switch (entry.State)
      {
        case EntityState.Added :
          entry.Entity.CreatedBy = _currentUserService.UserId;
          entry.Entity.Created = _dateTime.Now;

          break;

        case EntityState.Modified :
          entry.Entity.LastModifiedBy = _currentUserService.UserId;
          entry.Entity.LastModified = _dateTime.Now;

          break;

        case EntityState.Detached :  break;
        case EntityState.Unchanged : break;
        case EntityState.Deleted :   break;
        default :                    throw new ArgumentOutOfRangeException();
      }
    }

    var events = ChangeTracker.Entries<IHasDomainEvent>()
                              .Select(x => x.Entity.DomainEvents)
                              .SelectMany(x => x)
                              .Where(domainEvent => !domainEvent.IsPublished)
                              .ToArray();

    var result = await base.SaveChangesAsync(cancellationToken);

    await DispatchEvents(events);

    return result;
  }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);

    builder.Entity<Activity>()
           .ToTable("Activities");

    builder.Entity<ActivityAttendee>()
           .ToTable("ActivityAttendees");
  }

  async private Task DispatchEvents(DomainEvent[ ] events)
  {
    foreach (var @event in events)
    {
      @event.IsPublished = true;
      await _domainEventService.Publish(@event);
    }
  }
}

public class
    ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
  public ApplicationDbContext CreateDbContext(string[ ] args)
  {
    var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

    optionsBuilder
        .UseSqlServer("Server=localhost; Database=Events; User Id=sa; Password=Password123456789;Encrypt=False;TrustServerCertificate=true");

    return new ApplicationDbContext(optionsBuilder.Options);
  }
}
