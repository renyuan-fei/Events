using System.Text;

using Application;
using Application.common.interfaces;
using Application.common.Interfaces;
using Application.Common.Interfaces;
using Application.common.Security;

using FluentValidation.AspNetCore;

using Infrastructure.DatabaseContext;
using Infrastructure.Service;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Tokens;

using WebAPI.Filters;
using WebAPI.Services;

namespace WebAPI.StartupExtensions;

/// <summary>
///   Configuration
/// </summary>
public static class ConfigureServiceExtension
{
  /// <summary>
  ///   Configures the services for the application.
  /// </summary>
  /// <param name="services">The service collection.</param>
  /// <param name="configuration">The configuration to use.</param>
  /// <returns>The modified service collection.</returns>
  public static IServiceCollection ConfigureServices(
      this IServiceCollection services,
      IConfiguration          configuration)
  {
    #region DI
    services.AddTransient<IAuthorizationHandler, IsHostRequirementHandler>();

    // service
    services.AddSingleton<IConnectionManager, ConnectionManager>();
    services.AddSingleton<ICurrentUserService, CurrentUserService>();
    services.AddSingleton<INotificationService, NotificationService>();

    // AppIdentityDbContext
    services.AddScoped<IEventsDbContext, EventsDbContext>();
    // services.AddScoped<IAppIdentityDbContext, AppIdentityDbContext>();
    services.AddScoped<IAppIdentityDbContext, EventsDbContext>();

    // Token
    services.AddTransient<IJwtTokenService, JwtTokenService>();

    // ApplicationDI
    services.AddApplicationServices();
    // AutoMapper
    services.AddAutoMapper(typeof(Program));
    #endregion

    if (configuration.GetSection("ASPNETCORE_ENVIRONMENT").Value == "Development")
    {
      // services.AddControllersWithViews();
      services.AddControllersWithViews(options => options.Filters.Add<ApiExceptionFilterAttribute>())
              .AddFluentValidation(x => x.AutomaticValidationEnabled = false);
    }
    else
    {
      services.AddControllersWithViews(options =>
                                           options.Filters
                                                  .Add<ApiExceptionFilterAttribute>())
              .AddFluentValidation(x => x.AutomaticValidationEnabled = true);
    }

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
               .AllowCredentials()
               .WithHeaders("Authorization",
                            "origin",
                            "accept",
                            "content-type",
                            "x-requested-with",
                            "x-signalr-user-agent")
               .WithMethods("GET",
                            "POST",
                            "PUT",
                            "PATCH",
                            "OPTION",
                            "DELETE");
      });
    });

    // configuration for authentication
    services.AddAuthentication(options =>
            {
              options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

              options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
              options.TokenValidationParameters =
                  new TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = configuration["Jwt:Issuer"],
                      ValidAudience = configuration["Jwt:Audience"],
                      IssuerSigningKey =
                          new SymmetricSecurityKey(Encoding
                                                   .UTF8.GetBytes(configuration
                                                       ["Jwt:Key"]!))
                  };

              options.Events = new JwtBearerEvents
              {
                  OnMessageReceived = context =>
                  {
                    var accessToken = context.Request.Query["access_token"];
                    var path = context.HttpContext.Request.Path;

                    if (!string.IsNullOrEmpty(accessToken)
                     && (path.StartsWithSegments("/chat")
                      || path.StartsWithSegments("/notifications")))
                    {
                      context.Token = accessToken;
                    }

                    return Task.CompletedTask;
                  }
              };
            });

    services.AddAuthorization(opt =>
    {
      opt.AddPolicy("IsActivityHost",
                    policy =>
                    {
                      policy.Requirements
                            .Add(new IsHostRequirement());
                    });
    });

    // 添加 SignalR
    services.AddSignalR(opt =>
    {
      opt.EnableDetailedErrors = true;
      opt.KeepAliveInterval = TimeSpan.FromSeconds(10);
      opt.HandshakeTimeout = TimeSpan.FromSeconds(10);
    });

    services.AddHttpLogging(options =>
    {
      options.LoggingFields =
          HttpLoggingFields
              .RequestProperties
        | HttpLoggingFields
              .ResponsePropertiesAndHeaders;
    });

    return services;
  }
}
