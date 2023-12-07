namespace Application.common.Models;

/// <summary>
/// Represents the parameters for a paginated list.
/// </summary>
public class PaginatedListParams
{
  public bool IsGoing { get; set; }

  public bool IsHost { get; set; }

  public DateTime StartDate { get; set; } = DateTime.UtcNow;

  public int     PageNumber { get; init; }
  public int     PageSize   { get; init; }
  public string? SearchTerm { get; init; }
}
