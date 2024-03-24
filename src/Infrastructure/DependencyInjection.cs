using System.Reflection;

using Application.Common.Helpers;
using Application.common.interfaces;
using Application.common.Interfaces;
using Application.Common.Interfaces;
using Application.common.Security;

using Domain.Entities;
using Domain.Repositories;

using Infrastructure.Data.Interceptors;
using Infrastructure.DatabaseContext;
using Infrastructure.Identity;
using Infrastructure.Repositories;
using Infrastructure.security;
using Infrastructure.Service;

using Microsoft.AspNetCore.Authorization;
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

    // services.AddScoped<IAppIdentityDbContext>(provider => provider
    //                                               .GetRequiredService<
    //                                                   AppIdentityDbContext>());

    services.AddScoped<IAppIdentityDbContext>(provider => provider
                                                  .GetRequiredService<
                                                      EventsDbContext>());

    // unit of work
    services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<EventsDbContext>());

    // transaction manager
    services.AddScoped<ITransactionManager, TransactionManager>();

    // services
    services.AddTransient<IDateTimeService, DateTimeService>();
    services.AddTransient<IIdentityService, IdentityService>();
    services.AddScoped<ICloudinaryService, CloudinaryService>();
    services.AddScoped<IPhotoService, PhotoService>();
    services.AddScoped<IUserService, UserService>();

    // interceptor
    services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
    services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

    // used for auditing data that changed in domain events
    services.AddScoped<ISaveChangesInterceptor, PostDomainEventAuditingInterceptor>();

    // repository
    services.AddScoped<IActivityRepository, ActivityRepository>();
    services.AddScoped<IAttendeeRepository, AttendeeRepository>();
    services.AddScoped<IFollowingRepository, FollowingRepository>();
    services.AddScoped<IPhotoRepository, PhotoRepository>();
    services.AddScoped<ICommentRepository, CommentRepository>();
    services.AddScoped<IUserNotificationRepository, UserNotificationRepository>();
    services.AddScoped<INotificationRepository, NotificationRepository>();
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

      // services.AddDbContext<AppIdentityDbContext>(options =>
      //                                                 options
      //                                                     .UseInMemoryDatabase("Identity"));
    }
    else
    {
      string eventsDbConnection;

      var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
      // Depending on if in development or production, use either FlyIO
      // connection string, or development connection string from env var.
      if (env == "Development")
      {
        eventsDbConnection = configuration.GetConnectionString("EventsConnection");

        GuardValidation.AgainstNull(eventsDbConnection, message: "Connection string 'EventsConnection' not found.");
      }
      else
      {
        // Use connection string provided at runtime by Flyio.
        var connUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

        GuardValidation.AgainstNull(connUrl, message: "Connection string 'connUrl' not found.");

        // Parse connection URL to connection string for Npgsql
        connUrl = connUrl!.Replace("postgres://", string.Empty);
        var pgUserPass = connUrl.Split("@")[0];
        var pgHostPortDb = connUrl.Split("@")[1];
        var pgHostPort = pgHostPortDb.Split("/")[0];
        var pgDb = pgHostPortDb.Split("/")[1];
        var pgUser = pgUserPass.Split(":")[0];
        var pgPass = pgUserPass.Split(":")[1];
        var pgHost = pgHostPort.Split(":")[0];
        var pgPort = pgHostPort.Split(":")[1];

        eventsDbConnection = $"Server={pgHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb};SSL Mode=Disable";
      }

      services.AddDbContext<EventsDbContext>((sp, options) =>
      {
        options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

        options.UseNpgsql(eventsDbConnection, b => b.MigrationsAssembly(typeof(EventsDbContext).Assembly.FullName));
      });

      // services.AddDbContext<AppIdentityDbContext>(options =>
      // {
      //   options.UseNpgsql(identityDbConnection, b => b.MigrationsAssembly(typeof(AppIdentityDbContext).Assembly.FullName));
      // });
    }

    // configuration for Identity
    // services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
    //         {
    //           options.Password.RequiredLength = 10;
    //           options.Password.RequireNonAlphanumeric = false;
    //           options.Password.RequireUppercase = false;
    //           options.Password.RequireLowercase = true;
    //           options.Password.RequireDigit = true;
    //         })
    //         .AddEntityFrameworkStores<AppIdentityDbContext>()
    //         .AddDefaultTokenProviders()
    //         .AddUserStore<UserStore<ApplicationUser, ApplicationRole, AppIdentityDbContext
    //           , string>>()
    //         .AddRoleStore<RoleStore<ApplicationRole, AppIdentityDbContext, string>>();

    services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
              options.Password.RequiredLength = 10;
              options.Password.RequireNonAlphanumeric = false;
              options.Password.RequireUppercase = false;
              options.Password.RequireLowercase = true;
              options.Password.RequireDigit = true;
            })
            .AddEntityFrameworkStores<EventsDbContext>()
            .AddDefaultTokenProviders()
            .AddUserStore<UserStore<ApplicationUser, ApplicationRole, EventsDbContext
              , string>>()
            .AddRoleStore<RoleStore<ApplicationRole, EventsDbContext, string>>();


    return services;
  }
}
