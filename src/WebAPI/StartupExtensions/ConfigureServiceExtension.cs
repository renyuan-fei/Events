using Domain.Identity;

using Infrastructure.DatabaseContext;
using Infrastructure.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.StartupExtensions;

/// <summary>
/// Configuration
/// </summary>
public static class ConfigureServiceExtension
{
  /// <summary>
  /// Configuration
  /// </summary>
  /// <param name="services"></param>
  /// <param name="configuration"></param>
  /// <returns>
  /// All service collection
  /// </returns>
  public static IServiceCollection ConfigureServices(
      this IServiceCollection services,
      IConfiguration          configuration)
  {
    // Add database context
    services.AddDbContext<ApplicationDbContext>(options =>
    {
      options.UseSqlServer(configuration
                               .GetConnectionString("DefaultConnection")!);
    });

    // middleware
    #region Middleware
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    #endregion

    // configuration for Identity
    services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
              options.Password.RequiredLength = 5;
              options.Password.RequireNonAlphanumeric = false;
              options.Password.RequireUppercase = false;
              options.Password.RequireLowercase = true;
              options.Password.RequireDigit = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders()
            .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext
              , Guid>>()
            .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();

    return services;
  }
}
