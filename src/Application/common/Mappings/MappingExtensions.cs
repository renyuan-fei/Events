using Application.common.Models;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

namespace Application.common.Mappings;

/// <summary>
/// Provides extension methods for IQueryable, for pagination and projection (mapping) operations.
/// </summary>
public static class MappingExtensions
{
  /// <summary>
  /// Returns a paginated list of items from the given queryable data source.
  /// </summary>
  /// <typeparam name="TDestination">The type of the elements in the queryable.</typeparam>
  /// <param name="queryable">The queryable data source.</param>
  /// <param name="pageNumber">The page number of the items to retrieve.</param>
  /// <param name="pageSize">The number of items per page.</param>
  /// <returns>A task that represents the asynchronous operation. The task result contains a PaginatedList of type TDestination.</returns>
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
  /// Maps the results of an IQueryable to another type using AutoMapper.
  /// </summary>
  /// <param name="queryable">The query to be mapped.</param>
  /// <param name="configuration">The AutoMapper configuration provider.</param>
  /// <typeparam name="TDestination">The destination mapping type.</typeparam>
  /// <returns>A list of mapped data.</returns>
  public static Task<List<TDestination>> ProjectToListAsync <TDestination>(
      this IQueryable        queryable,
      IConfigurationProvider configuration) where TDestination : class
  {
    return queryable.ProjectTo<TDestination>(configuration).AsNoTracking().ToListAsync();
  }
}
