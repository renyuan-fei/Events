using Application.Common.Interfaces;

using Domain.Entities;
using Domain.Identity;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.DatabaseContext;

public class ApplicationDbContext :
    IdentityDbContext<ApplicationUser, ApplicationRole, Guid>,
    IApplicationDbContext
{
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
      base(options)
  {
  }

  public ApplicationDbContext() { }

  public DbSet<Activity>         Activities        => Set<Activity>();
  public DbSet<ActivityAttendee> ActivityAttendees => Set<ActivityAttendee>();

  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);

    builder.Entity<Activity>()
           .ToTable("Activities");

    builder.Entity<ActivityAttendee>()
           .ToTable("ActivityAttendees");
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
