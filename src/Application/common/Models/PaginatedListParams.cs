namespace Application.common.Models;

/// <summary>
///   Represents the parameters for a paginated list.
/// </summary>
public class PaginatedListParams
{
  public DateTime InitialTimestamp { get; init; } = DateTime.MaxValue;
  public int PageNumber { get; set; } = 1;
  public int PageSize   { get; set; } = 10;
}
