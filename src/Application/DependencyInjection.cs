using System.Reflection;

using Application.common.Behaviours;

using FluentValidation;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

namespace Application;

/// <summary>
/// Provides methods for adding application services to the <see cref="IServiceCollection"/>.
/// </summary>
public static class DependencyInjection
{
  /// <summary>
  /// Extension method to add application services to the <see cref="IServiceCollection"/>.
  /// </summary>
  /// <param name="services">The <see cref="IServiceCollection"/> to add the application services to.</param>
  /// <returns>The modified <see cref="IServiceCollection"/> with the application services added.</returns>
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

      // 注册全局异常处理器
      cfg.AddBehavior(typeof(IPipelineBehavior<,>),
                      typeof(UnhandledExceptionBehaviour<,>));

      // 应用授权验证器
      cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));

      // 应用 validators 验证器
      cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

      // 应用 性能 验证验证器
      cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
    });

    return services;
  }
}
