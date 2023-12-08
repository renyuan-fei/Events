using Microsoft.EntityFrameworkCore;

namespace Application.common.Models;

/// <summary>
///   Represents a generic class for paginated data.
/// </summary>
/// <typeparam name="T">The type of elements in the list.</typeparam>
public class PaginatedList <T>
{
  public PaginatedList() { }

  public PaginatedList(
      IReadOnlyCollection<T> items,
      int                    count,
      int                    pageNumber,
      int                    pageSize)
  {
    PageNumber = pageNumber;
    TotalPages = (int)Math.Ceiling(count / (double)pageSize);
    TotalCount = count;
    Items = items;
  }

  public IReadOnlyCollection<T> Items      { get; }
  public int                    PageNumber { get; }
  public int                    TotalPages { get; }
  public int                    TotalCount { get; }

  public bool HasPreviousPage => PageNumber > 1;

  public bool HasNextPage => PageNumber < TotalPages;

  public async static Task<PaginatedList<T>> CreateAsync(
      IQueryable<T> source,
      int           pageNumber,
      int           pageSize)
  {
    var count = await source.CountAsync();

    var items = await source.Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();

    return new PaginatedList<T>(items, count, pageNumber, pageSize);
  }
}
