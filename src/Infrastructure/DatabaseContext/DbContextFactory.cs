using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.DatabaseContext;

/// <summary>
///   Factory class for creating an instance of <see cref="AppIdentityDbContext" /> for
///   design-time scenarios.
/// </summary>
public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<EventsDbContext>
{
  /// <summary>
  ///   Creates a new instance of the AppIdentityDbContext class.
  /// </summary>
  /// <param name="args">The arguments passed to the method.</param>
  /// <returns>A new instance of the AppIdentityDbContext class.</returns>
  public EventsDbContext CreateDbContext(string[ ] args)
  {
    var optionsBuilder = new DbContextOptionsBuilder<EventsDbContext>();

    // optionsBuilder.UseSqlServer("Server=localhost; Database=Events; User Id=sa; Password=Password123456789;Encrypt=False;TrustServerCertificate=true");
    optionsBuilder.UseNpgsql("Server=localhost; Database=Events; User Id=sa; Password=Password123456789;Encrypt=False;TrustServerCertificate=true");

    return new EventsDbContext(optionsBuilder.Options);
  }
}
