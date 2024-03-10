namespace Application.common.DTO;

public class NotificationDto
{
  public string Context { get; set; } = string.Empty;
  public string RelatedId { get; set; } = string.Empty;
  public bool Status { get; set; }
}
