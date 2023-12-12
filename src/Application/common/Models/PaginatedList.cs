using System.Collections;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace Application.common.Models;

/// <summary>
///   Represents a generic class for paginated data.
/// </summary>
/// <typeparam name="T">The type of elements in the list.</typeparam>
public class PaginatedList <T> : ICollection<T>
{
  private IReadOnlyCollection<T> _items;

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
    _items = items;
  }

  public IReadOnlyCollection<T> Items      
  {
      get { return _items; } 
      private set { _items = value; } 
  }

  public int PageNumber { get; }
  
  public int TotalPages { get; }
  
  public int TotalCount { get; }

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

    return new PaginatedList<T>((IReadOnlyCollection<T>)items, count, pageNumber, pageSize);
  }

  public IEnumerator<T> GetEnumerator() => Items.GetEnumerator();

  public void Add(T item) => throw new NotSupportedException();

  public void Clear() => throw new NotSupportedException();

  public bool Contains(T item) => _items.Contains(item);

  public void CopyTo(T[] array, int arrayIndex) => _items.ToList().CopyTo(array, arrayIndex);

  public bool Remove(T item) => throw new NotSupportedException();

  public int Count => _items.Count;

  public bool IsReadOnly => true;

  IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

  public T this[int index] => _items.ElementAt(index);
}