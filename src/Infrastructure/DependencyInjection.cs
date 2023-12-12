using Application.common.interfaces;
using Application.common.Interfaces;

using Infrastructure.DatabaseContext;
using Infrastructure.Identity;
using Infrastructure.security;
using Infrastructure.Service;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(
      this IServiceCollection services,
      IConfiguration          configuration)
  {
    #region DI
    services.AddScoped<IDomainEventService, DomainEventService>();
    services.AddTransient<IDateTime, DateTimeService>();
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<ICloudinaryService, CloudinaryService>();
    #endregion

    services.Configure<CloudinarySettings>(configuration.GetSection("Cloudinary"));


    // Add database context
    if (configuration.GetValue<bool>("UseInMemoryDatabase"))
    {
      services.AddDbContext<EventsDbContext>(options =>
                                                 options
                                                     .UseInMemoryDatabase("Events"));

      services.AddDbContext<AppIdentityDbContext>(options =>
                                                      options
                                                          .UseInMemoryDatabase("Identity"));
    }
    else
    {
      services.AddDbContext<EventsDbContext>(options =>
                                                 options.UseSqlServer(configuration
                                                          .GetConnectionString("EventsConnection"),
                                                      b =>
                                                          b.MigrationsAssembly(typeof
                                                                       (EventsDbContext)
                                                                   .Assembly
                                                                   .FullName)));

      services.AddDbContext<AppIdentityDbContext>(options =>
                                                      options.UseSqlServer(configuration
                                                               .GetConnectionString("IdentityConnection"),
                                                           b =>
                                                               b.MigrationsAssembly(typeof
                                                                            (AppIdentityDbContext)
                                                                        .Assembly
                                                                        .FullName)));
    }

    // configuration for Identity
    services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
              options.Password.RequiredLength = 5;
              options.Password.RequireNonAlphanumeric = false;
              options.Password.RequireUppercase = false;
              options.Password.RequireLowercase = true;
              options.Password.RequireDigit = true;
            })
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddDefaultTokenProviders()
            .AddUserStore<UserStore<ApplicationUser, ApplicationRole, AppIdentityDbContext
              , Guid>>()
            .AddRoleStore<RoleStore<ApplicationRole, AppIdentityDbContext, Guid>>();

    services.AddTransient<IIdentityService, IdentityService>();

    return services;
  }
}
