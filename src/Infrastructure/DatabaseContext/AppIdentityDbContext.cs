using Application.common.Interfaces;

using Infrastructure.Identity;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DatabaseContext;

/// <summary>
///   Represents the database context for the application.
/// </summary>
/// /
public class AppIdentityDbContext :
    IdentityDbContext<ApplicationUser, ApplicationRole, Guid>,
    IAppIdentityDbContext
{
  public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) :
      base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);
    // Customize the ASP.NET Identity model and override the defaults if needed.
    // For example, you can rename the ASP.NET Identity table names and more.
    // Add your customizations after calling base.OnModelCreating(builder);
  }
}
