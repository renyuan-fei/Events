using System.Text;

using Application;
using Application.common.interfaces;
using Application.Common.Interfaces;

using FluentValidation.AspNetCore;

using Infrastructure.DatabaseContext;
using Infrastructure.Service;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
  ///   Configuration
  /// </summary>
  /// <param name="services"></param>
  /// <param name="configuration"></param>
  /// <returns>
  ///   All service collection
  /// </returns>
  [ Obsolete("Obsolete") ]
  public static IServiceCollection ConfigureServices(
      this IServiceCollection services,
      IConfiguration          configuration)
  {
    #region DI
    // Current user service
    services.AddSingleton<ICurrentUserService, CurrentUserService>();

    // ApplicationDbContext
    services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

    // Token
    services.AddTransient<IJwtTokenService, JwtTokenService>();

    // ApplicationDI
    services.AddApplicationServices();
    // AutoMapper
    services.AddAutoMapper(typeof(Program));
    #endregion

    // services.AddControllersWithViews();
    services.AddControllersWithViews(options =>
                                         options.Filters
                                                .Add<ApiExceptionFilterAttribute>())
            .AddFluentValidation(x => x.AutomaticValidationEnabled = false);

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
              // 设置默认身份验证方案为Cookie身份验证
              options.DefaultAuthenticateScheme =
                  CookieAuthenticationDefaults.AuthenticationScheme;

              options.DefaultChallengeScheme =
                  CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
              // 设置Cookie选项
              options.Cookie.HttpOnly = true; // 设置Cookie为HttpOnly，提高安全性

              options.Cookie.SecurePolicy =
                  CookieSecurePolicy.Always; // 设置Cookie仅在HTTPS下有效

              // 配置Cookie身份验证事件
              options.Events = new CookieAuthenticationEvents
              {
                  // 当验证Cookie时触发
                  OnValidatePrincipal = async context =>
                  {
                    // 从DI容器中获取JWT Token服务
                    var jwtTokenService = context.HttpContext.RequestServices
                                                 .GetRequiredService<IJwtTokenService>();

                    // 从请求中获取名为"JwtToken"的Cookie
                    var cookie = context.Request.Cookies["JwtToken"];

                    // 如果Cookie不为空，则尝试验证Token
                    if (!string.IsNullOrEmpty(cookie))
                    {
                      try
                      {
                        // 使用JWT服务从Token中获取ClaimsPrincipal
                        var principal = jwtTokenService.GetPrincipalFromJwtToken(cookie);

                        // 替换当前的用户身份为新的ClaimsPrincipal
                        context.ReplacePrincipal(principal);

                        // 设置ShouldRenew为true，表示身份验证需要刷新
                        context.ShouldRenew = true;
                      }
                      catch
                      {
                        // 验证失败时拒绝用户身份
                        context.RejectPrincipal();
                      }
                    }
                  }
              };
            })
            .AddJwtBearer(options =>
            {
              options.TokenValidationParameters =
                  new TokenValidationParameters
                  {
                      ValidateAudience = true,
                      ValidAudience = configuration["Jwt:Audience"],
                      ValidateIssuer = true,
                      ValidIssuer = configuration["Jwt:Issuer"],
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      IssuerSigningKey =
                          new SymmetricSecurityKey(Encoding
                                                   .UTF8.GetBytes(configuration
                                                       ["Jwt:Key"]!))
                  };
            });

    services.AddAuthorization(options => { });

    return services;
  }
}
