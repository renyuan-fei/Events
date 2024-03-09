namespace Domain.ValueObjects.Message;

public record UserNotificationId(string Value)
{
  public static UserNotificationId New() { return new UserNotificationId(Guid.NewGuid().ToString()); }
}
