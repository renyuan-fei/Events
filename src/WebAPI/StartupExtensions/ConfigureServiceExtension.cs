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
  /// Configures the services for the application.
  /// </summary>
  /// <param name="services">The service collection.</param>
  /// <param name="configuration">The configuration to use.</param>
  /// <returns>The modified service collection.</returns>
  public static IServiceCollection ConfigureServices(
      this IServiceCollection services,
      IConfiguration          configuration)
  {
    #region DI
    //
    services.AddTransient<IAuthorizationHandler, IsHostRequirementHandler>();

    // Current user service
    services.AddSingleton<ICurrentUserService, CurrentUserService>();

    // AppIdentityDbContext
    services.AddScoped<IEventsDbContext, EventsDbContext>();
    services.AddScoped<IAppIdentityDbContext, AppIdentityDbContext>();

    // Token
    services.AddTransient<IJwtTokenService, JwtTokenService>();

    // ApplicationDI
    services.AddApplicationServices();
    // AutoMapper
    services.AddAutoMapper(typeof(Program));
    #endregion


    if(configuration.GetSection("ASPNETCORE_ENVIRONMENT").Value == "Development")
    {
      services.AddControllersWithViews();
    }
    else
    {
      services.AddControllersWithViews(options =>
                                           options.Filters
                                                  .Add<ApiExceptionFilterAttribute>())
              .AddFluentValidation(x => x.AutomaticValidationEnabled = false);

      services.AddDataProtection()
              .ProtectKeysWithCertificate("thumbprint");
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
               .WithHeaders("Authorization", "origin", "accept", "content-type")
               .WithMethods("GET",
                            "POST",
                            "PUT",
                            "DELETE",
                            "OPTIONS");
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

              options.Events = new JwtBearerEvents();
            });

    services.AddAuthorization(opt =>
    {
      opt.AddPolicy("IsActivityHost", policy =>
      {
        policy.Requirements.Add(new IsHostRequirement());
      });
    });

    services.AddHttpLogging(options =>
    {
      options.LoggingFields =
          Microsoft.AspNetCore.HttpLogging.HttpLoggingFields
                   .RequestProperties
        | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields
                   .ResponsePropertiesAndHeaders;
    });

    return services;
  }
}
