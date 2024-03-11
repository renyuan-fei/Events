using Application.common.DTO;

namespace Application.common.Interfaces;

/// <summary>
/// Represents a service that handles the sending of notifications.
/// </summary>
public interface INotificationService
{
  /// <summary>
  /// Sends an activity notification to all users in a specified activity group.
  /// </summary>
  /// <param name="methodName">The name of the method to trigger in the clients.</param>
  /// <param name="groupName"></param>
  /// <param name="excludedUserId"></param>
  /// <param name="notification"></param>
  /// <returns>A task that represents the asynchronous operation.</returns>
  Task SendActivityNotificationToAll(
      string          methodName,
      string          groupName,
      NotificationDto notification,
      List<string>?   excludedUserId = null);

  /// <summary>
  /// Sends a message to a specific user using SignalR.
  /// </summary>
  /// <param name="methodName">The name of the method to be invoked on the client.</param>
  /// <param name="userId">The ID of the user to send the message to.</param>
  /// <param name="notification"></param>
  /// <returns>A task representing the asynchronous operation.</returns>
  public Task SendMessageToUser(
      string          methodName,
      string          userId,
      NotificationDto notification);
}
