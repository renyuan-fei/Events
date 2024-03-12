using Domain.Enums;

namespace Application.common.DTO;

public class NotificationDto
{
  public string Id { get; set; } = string.Empty;
  public string Content { get; set; } = string.Empty;
  public string RelatedId { get; set; } = string.Empty;
  public NotificationType Type { get; set; } = NotificationType.Default;

  public DateTimeOffset Created { get; set; } = DateTimeOffset.MaxValue;
  public bool Status { get; set; }
}
