using System.Reflection;

using Application.common.Behaviours;

using FluentValidation;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

namespace Application;


public static class DependencyInjection
{
  public static IServiceCollection AddApplicationServices(
      this IServiceCollection services)
  {
    // 读取该程序集中 所有关于 AutoMapper 的 配置文件 (继承Profile)
    services.AddAutoMapper(Assembly.GetExecutingAssembly());

    // 自动注册所有 FluentValidation 验证器
    services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

    services.AddMediatR(cfg =>
    {
      // 注册 MediatR 处理程序
      cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

      // 应用 validators 验证器
      cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

    });

    return services;
  }
}
