using System.Reflection;

using Application.Common.Interfaces;
using Application.CQRS.Activities.Queries;
using Application.CQRS.Activities.Queries.GetActivity;

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
    #region DI
    // ApplicationDbContext
    services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
    // MediaR
    services.AddMediatR(cfg =>
    {
      cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

      cfg.RegisterServicesFromAssembly(typeof(GetAllActivitiesQueryHandler).Assembly);
    });
    #endregion

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

    // 对跨域请求进行配置
    // CORS; localhost:7173
    services.AddCors(options =>
    {
      options.AddDefaultPolicy(builder =>
      {
        // 允许端口 5173 进行跨域请求
        builder.WithOrigins(configuration.GetSection("AllowedOrigins").Get<string[ ]>()!)
               .WithHeaders("Authorization", "origin", "accept", "content-type")
               .WithMethods("GET",
                            "POST",
                            "PUT",
                            "DELETE",
                            "OPTIONS");
      });
    });

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
