using System.Reflection;

using Application.common.interfaces;
using Application.common.Interfaces;
using Application.Common.Interfaces;

using Domain.Repositories;

using Infrastructure.DatabaseContext;
using Infrastructure.Identity;
using Infrastructure.Interceptors;
using Infrastructure.Repositories;
using Infrastructure.security;
using Infrastructure.Service;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
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
    // DbContext
    services.AddScoped<IEventsDbContext>(provider => provider
                                             .GetRequiredService<EventsDbContext>());

    services.AddScoped<IAppIdentityDbContext>(provider => provider
                                                  .GetRequiredService<
                                                      AppIdentityDbContext>());

    // unit of work
    services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<EventsDbContext>());

    // transaction manager
    services.AddScoped<ITransactionManager, TransactionManager>();

    // services
    services.AddTransient<IDateTimeService, DateTimeService>();
    services.AddScoped<ICloudinaryService, CloudinaryService>();
    services.AddScoped<IPhotoService, PhotoService>();
    services.AddScoped<IUserService, UserService>();

    // interceptor
    services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
    services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

    // repository
    services.AddScoped<IActivityRepository, ActivityRepository>();
    services.AddScoped<IAttendeeRepository, AttendeeRepository>();
    services.AddScoped<IFollowingRepository, FollowingRepository>();
    services.AddScoped<IPhotoRepository, PhotoRepository>();
    #endregion

    // read and config all mapping settings that inherit from Class Profile
    services.AddAutoMapper(Assembly.GetExecutingAssembly());

    // read configuration of Cloudinary from appsettings.json
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
      var eventsDbConnection = configuration.GetConnectionString("EventsConnection");

      Guard.Against.Null(eventsDbConnection,
                         message: "Connection string 'EventsConnection' not found.");

      var identityDbConnection = configuration.GetConnectionString("IdentityConnection");

      Guard.Against.Null(identityDbConnection,
                         message: "Connection string 'IdentityConnection' not found.");

      services.AddDbContext<EventsDbContext>((sp, options) =>
      {
        options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

        options.UseSqlServer(eventsDbConnection,
                             b =>
                                 b.MigrationsAssembly(typeof(EventsDbContext)
                                                      .Assembly
                                                      .FullName));
      });

      services.AddDbContext<AppIdentityDbContext>(options =>
                                                      options
                                                          .UseSqlServer(identityDbConnection,
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
              , string>>()
            .AddRoleStore<RoleStore<ApplicationRole, AppIdentityDbContext, string>>();

    services.AddTransient<IIdentityService, IdentityService>();

    return services;
  }
}
