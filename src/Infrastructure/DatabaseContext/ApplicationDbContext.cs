using Application.common.interfaces;
using Application.Common.Interfaces;

using Domain.Common;
using Domain.Entities;

using Duende.IdentityServer.EntityFramework.Options;

using Infrastructure.Identity;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;

namespace Infrastructure.DatabaseContext;

/// <summary>
/// Represents the database context for the application. </summary>
/// /
public class ApplicationDbContext :
    IdentityDbContext<ApplicationUser, ApplicationRole, Guid>,
    IApplicationDbContext
{
  /// <summary>
  /// Represents a service that provides access to the current user information.
  /// </summary>
  private readonly ICurrentUserService _currentUserService;

  /// <summary>
  /// Represents an instance of a DateTime object.
  /// </summary>
  private readonly IDateTime _dateTime;

  /// <summary>
  /// Represents a private readonly field of type IDomainEventService that is used for handling domain events.
  /// </summary>
  private readonly IDomainEventService _domainEventService;

  /// <summary>
  /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
  /// </summary>
  /// <param name="options">The options for configuring the <see cref="ApplicationDbContext"/>.</param>
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
      base(options)
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
  /// </summary>
  /// <param name="options">The <see cref="DbContextOptions{ApplicationDbContext}"/> used to configure the context.</param>
  /// <param name="operationalStoreOptions">The <see cref="IOptions{OperationalStoreOptions}"/> used to configure the operational store options.</param>
  /// <param name="currentUserService">The <see cref="ICurrentUserService"/> used to access the current user information.</param>
  /// <param name="dateTime">The <see cref="IDateTime"/> used to access the current date and time.</param>
  /// <param name="domainEventService">The <see cref="IDomainEventService"/> used for handling domain events.</param>
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

  /// <summary>
  /// Initializes a new instance of the ApplicationDbContext class.
  /// </summary>
  /// <param name="currentUserService">An implementation of the ICurrentUserService interface.</param>
  /// <param name="dateTime">An implementation of the IDateTime interface.</param>
  /// <param name="domainEventService">An implementation of the IDomainEventService interface.</param>
  public ApplicationDbContext(
      ICurrentUserService currentUserService,
      IDateTime           dateTime,
      IDomainEventService domainEventService)
  {
    _currentUserService = currentUserService;
    _dateTime = dateTime;
    _domainEventService = domainEventService;
  }

  /// <summary>
  /// Gets or sets the activities in the database.
  /// </summary>
  /// <value>
  /// The activities.
  /// </value>
  public DbSet<Activity> Activities => Set<Activity>();

  /// <summary>
  /// Gets or sets the DbSet of <see cref="ActivityAttendee"/>.
  /// </summary>
  /// <returns>The DbSet of <see cref="ActivityAttendee"/>.</returns>
  public DbSet<ActivityAttendee> ActivityAttendees => Set<ActivityAttendee>();

  /// <summary>
  /// Asynchronously saves changes to the database and performs additional operations
  /// such as setting audit fields and dispatching domain events.
  /// </summary>
  /// <param name="cancellationToken">The cancellation token for the asynchronous operation.</param>
  /// <returns>A task that represents the asynchronous operation.
  /// The task result contains the number of objects saved to the database.</returns>
  public async override Task<int> SaveChangesAsync(
      CancellationToken cancellationToken = new())
  {
    foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
    {
      switch (entry.State)
      {
        case EntityState.Added :

          entry.Entity.Created = _dateTime.Now;
          entry.Entity.CreatedBy = _currentUserService.UserId;

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

  /// <summary>
  /// This method is called by the framework when the model for a derived context has been initialized, but before the model has been locked down and used to initialize the context. It
  /// allows you to further configure the model and its entities.
  /// </summary>
  /// <param name="builder">The builder used to construct the model for this context.</param>
  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);

    builder.Entity<Activity>()
           .HasMany(e => e.Attendees)
           .WithOne(e => e.Activity)
           .HasForeignKey(e => e.Id);

    builder.Entity<ActivityAttendee>()
           .HasOne(e => e.Activity)
           .WithMany(e => e.Attendees)
           .HasForeignKey(e => e.Id)
           .OnDelete(DeleteBehavior.Cascade);
  }

  /// <summary>
  /// Dispatches the given domain events by setting their IsPublished property to true and
  /// publishing them using the _domainEventService.
  /// </summary>
  /// <param name="events">The domain events to be dispatched.</param>
  /// <returns>A Task representing the asynchronous operation.</returns>
  async private Task DispatchEvents(DomainEvent[ ] events)
  {
    foreach (var @event in events)
    {
      @event.IsPublished = true;
      await _domainEventService.Publish(@event);
    }
  }
}

/// <summary>
/// Factory class for creating an instance of <see cref="ApplicationDbContext"/> for design-time scenarios.
/// </summary>
public class
    ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
  /// <summary>
  /// Creates a new instance of the ApplicationDbContext class.
  /// </summary>
  /// <param name="args">The arguments passed to the method.</param>
  /// <returns>A new instance of the ApplicationDbContext class.</returns>
  public ApplicationDbContext CreateDbContext(string[ ] args)
  {
    var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

    optionsBuilder
        .UseSqlServer("Server=localhost; Database=Events; User Id=sa; Password=Password123456789;Encrypt=False;TrustServerCertificate=true");

    return new ApplicationDbContext(optionsBuilder.Options);
  }
}
