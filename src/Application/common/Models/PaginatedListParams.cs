namespace Application.common.Models;

/// <summary>
///   Represents the parameters for a paginated list.
/// </summary>
public class PaginatedListParams
{
  public bool IsGoing { get; set; } = false;

  public bool IsHost { get; set; } = false;

  public string?   Title     { get; init; }
  public string?   Category  { get; init; }
  public string?   City      { get; init; }
  public string?   Venue     { get; init; }
  public DateTime? StartDate { get; init; } // 可选的日期范围开始
  public DateTime? EndDate   { get; init; } // 可选的日期范围结束

  public int PageNumber { get; set; }
  public int PageSize   { get; init; } = 10;
}
