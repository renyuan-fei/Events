using Application.common.DTO;
using Application.common.Interfaces;

using Microsoft.AspNetCore.SignalR;

using WebAPI.SignalR;

namespace WebAPI.Services;

public class NotificationService : INotificationService
{
  private readonly IHubContext<NotificationHub> _hubContext;

  public NotificationService(IHubContext<NotificationHub> hubContext)
  {
    _hubContext = hubContext;
  }

  public async Task SendActivityNotificationToAll(
      string methodName,
      string groupName,
      string activityId,
      string message)
  {
    await _hubContext.Clients.Group(groupName).SendAsync(methodName, new NotificationDto
    {
        Status = false,
        Context = message,
        RelatedId = activityId
    });
  }

  public async Task SendMessageToUser(
      string methodName,
      string userId,
      string parameter,
      string message)
  {
    // 使用SignalR的User方法定位用户，这里假设userId即为SignalR分配给用户的组名（通常可以在用户连接时设置）
    await _hubContext.Clients.User(userId).SendAsync(methodName, new NotificationDto
    {
        Status = false,
        Context = message,
        RelatedId = parameter
    });
  }

}
