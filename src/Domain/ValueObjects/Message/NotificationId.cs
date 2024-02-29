namespace Domain.ValueObjects.Message;


public record NotificationId(string Value)
{
  public static NotificationId New() { return new NotificationId(Guid.NewGuid().ToString()); }
}
