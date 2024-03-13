using Domain.Enums;

namespace Application.common.DTO;

public class NotificationDto
{
  public string Id { get; set; } = string.Empty;
  public string Content { get; set; } = string.Empty;
  public string RelatedId { get; set; } = string.Empty;
  public string Type { get; set; } = "Default";

  public DateTime Created { get; set; } = DateTime.MaxValue;
  public bool Status { get; set; }
}
