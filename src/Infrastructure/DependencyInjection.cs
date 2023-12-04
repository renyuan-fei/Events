using Application.common.interfaces;

using Infrastructure.DatabaseContext;
using Infrastructure.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(
      this IServiceCollection services,
      IConfiguration          configuration)
  {
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

    services.AddTransient<IIdentityService, IdentityService>();

    return services;
  }
}
