using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
  public static IServiceCollection AddApplicationServices(
      this IServiceCollection services)
  {
    // 读取该程序集中 所有关于 AutoMapper 的 配置文件 (继承Profile)
    services.AddAutoMapper(Assembly.GetExecutingAssembly());

    services.AddMediatR(cfg =>
    {
      cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    });

    return services;
  }
}
