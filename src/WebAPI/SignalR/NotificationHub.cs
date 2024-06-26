using Application.Common.Helpers;
using Application.common.interfaces;
using Application.common.Interfaces;
using Application.common.Models;
using Application.CQRS.Activities.Queries.GetUserParticipatedActivitiesQuery;
using Application.CQRS.Followers.Queries;
using Application.CQRS.Notifications.Commands;
using Application.CQRS.Notifications.Queries;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace WebAPI.SignalR;

//当使用SignalR时，服务器可以发送一个广播给所有连接的客户端，通知他们未读通数已经改变。客户端可以基于这个信息来更新其本地状态。

[ Application.common.Security.Authorize ]
public class NotificationHub : Hub
{
  private readonly IConnectionManager       _connectionManager;
  private readonly ILogger<NotificationHub> _logger;
  private readonly IMediator                _mediator;
  private readonly ICurrentUserService      _currentUserService;

  public NotificationHub(
      IMediator                mediator,
      ICurrentUserService      currentUserService,
      ILogger<NotificationHub> logger,
      IConnectionManager       connectionManager)
  {
    _mediator = mediator;
    _currentUserService = currentUserService;
    _logger = logger;
    _connectionManager = connectionManager;
  }

  public async override Task OnConnectedAsync()
  {
    var userId = _currentUserService.Id!;

    _connectionManager.AddConnection(userId, Context.ConnectionId);

    var activityIds = await _mediator.Send(new GetUserParticipatedActivitiesQuery { UserId = userId });

    var followerIds = await _mediator.Send(new GetFollowersIdQuery { UserId = userId });

    var unreadNotificationCount = await _mediator.Send(new GetUnreadNotificationNumberQuery { UserId = userId });

    await Groups.AddToGroupAsync(Context.ConnectionId, userId);
    await Groups.AddToGroupAsync(Context.ConnectionId, $"follower-{userId}");

    foreach (var followerId in followerIds)
    {
      await Groups.AddToGroupAsync(Context.ConnectionId, $"follower-{followerId}");
    }

    foreach (var activityId in activityIds)
    {
      await Groups.AddToGroupAsync(Context.ConnectionId, $"activity-{activityId}");
    }

    await Clients.Caller.SendAsync("LoadUnreadNotificationNumber", unreadNotificationCount);
  }

  public async override Task OnDisconnectedAsync(Exception exception)
  {
    _connectionManager.RemoveConnection(Context.ConnectionId);
    await base.OnDisconnectedAsync(exception);
  }

  public async Task ReadNotification(string userNotificationId)
  {
    var userId = _currentUserService.Id!;

    GuardValidation.AgainstNull(userNotificationId,
                                "notification with id cannot be null");

    // update notification status to read
    await _mediator.Send(new UpdateNotificationStatusCommand
    {
        UserId = userId,
        NotificationId = userNotificationId
    });

    await Clients.Caller.SendAsync("UpdateUnreadNotificationNumber", -1);
  }
  public async Task GetPaginatedNotifications(int pageNumber, int pageSize, DateTime? initialTimestamp)
  {
    var userId = _currentUserService.Id!;

    var paginatedNotifications = await _mediator.Send(new GetPaginatedNotificationQuery
    {
        UserId = userId,
        PaginatedListParams = new PaginatedListParams
        {
            InitialTimestamp = initialTimestamp ?? DateTime.MaxValue,
            PageNumber = pageNumber,
            PageSize = pageSize
        }
    });

    // 发送分页结果回客户端
    await Clients.Caller.SendAsync("LoadPaginatedNotifications", paginatedNotifications);
  }

  public async Task GetNotifications(int pageNumber, int pageSize)
  {
    var userId = _currentUserService.Id!;

    var query = new GetNotificationQuery
    {
        UserId = userId,
        PaginatedListParams = new PaginatedListParams
        {
            PageNumber = pageNumber,
            PageSize = pageSize
        }
    };

    var Notifications = await _mediator.Send(query);

    // 发送分页结果回客户端
    await Clients.Caller.SendAsync("LoadNotifications", Notifications);
  }
}
