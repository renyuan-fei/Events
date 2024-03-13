using Application.common.DTO;
using Application.common.Interfaces;

using Infrastructure.Data.Migrations;

using Microsoft.AspNetCore.SignalR;

using WebAPI.SignalR;

namespace WebAPI.Services;

public class NotificationService : INotificationService
{
  private readonly IConnectionManager       _connectionManager;
  private readonly IHubContext<NotificationHub> _hubContext;

  public NotificationService(IHubContext<NotificationHub> hubContext, IConnectionManager connectionManager)
  {
    _hubContext = hubContext;
    _connectionManager = connectionManager;
  }

  public async Task SendActivityNotificationToAll(
      string          methodName,
      string          groupName,
      NotificationDto notification,
      List<string>?   excludedUserIds)
  {
    excludedUserIds ??= new List<string>();

    var excludedConnectionIds = _connectionManager.GetConnectionsForUsers(excludedUserIds);

    await _hubContext.Clients.GroupExcept(groupName,excludedConnectionIds).SendAsync(methodName,
        notification);

    await _hubContext.Clients.GroupExcept(groupName, excludedConnectionIds).SendAsync("UpdateUnreadNotificationNumber",1);
  }

  public async Task SendMessageToUser(
      string methodName,
      string userId,
      NotificationDto notification)
  {
    // 使用SignalR的User方法定位用户，这里假设userId即为SignalR分配给用户的组名（通常可以在用户连接时设置）
    await _hubContext.Clients.User(userId).SendAsync(methodName, notification);

    await _hubContext.Clients.User(userId).SendAsync("UpdateUnreadNotificationNumber",1);

  }
}
