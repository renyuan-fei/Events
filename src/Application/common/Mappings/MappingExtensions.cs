using Application.common.Models;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

namespace Application.common.Mappings;

/// <summary>
///   提供 IQueryable 的扩展方法，用于分页和投影（映射）操作
/// </summary>
public static class MappingExtensions
{
  /// <summary>
  ///   将 IQueryable&lt;TDestination&gt; 转换为分页列表
  /// </summary>
  /// <param name="queryable">要分页的查询</param>
  /// <param name="pageNumber">当前页码</param>
  /// <param name="pageSize">每页的大小</param>
  /// <typeparam name="TDestination">分页列表中的元素类型。</typeparam>
  /// <returns>分页后的列表</returns>
  public static Task<PaginatedList<TDestination>> PaginatedListAsync <TDestination>(
      this IQueryable<TDestination> queryable,
      int                           pageNumber,
      int                           pageSize) where TDestination : class
  {
    return PaginatedList<TDestination>.CreateAsync(queryable.AsNoTracking(),
                                                   pageNumber,
                                                   pageSize);
  }

  /// <summary>
  ///   将 IQueryable 的结果使用 AutoMapper 映射到另一种类型
  /// </summary>
  /// <param name="queryable">要映射的查询</param>
  /// <param name="configuration">AutoMapper 配置提供者</param>
  /// <typeparam name="TDestination">目标映射类型</typeparam>
  /// <returns>映射后的数据列表</returns>
  public static Task<List<TDestination>> ProjectToListAsync <TDestination>(
      this IQueryable        queryable,
      IConfigurationProvider configuration) where TDestination : class
  {
    return queryable.ProjectTo<TDestination>(configuration).AsNoTracking().ToListAsync();
  }
}
