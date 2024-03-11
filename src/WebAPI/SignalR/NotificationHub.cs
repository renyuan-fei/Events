using Application.Common.Helpers;
using Application.common.interfaces;
using Application.common.Interfaces;
using Application.common.Security;
using Application.CQRS.Activities.Queries.GetUserParticipatedActivitiesQuery;
using Application.CQRS.Followers.Queries;
using Application.CQRS.Notifications.Commands;
using Application.CQRS.Notifications.Queries;

using Microsoft.AspNetCore.SignalR;

namespace WebAPI.SignalR;

[ Authorize ]
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

    var activityIds =
        await _mediator.Send(new GetUserParticipatedActivitiesQuery { UserId = userId });

    var followerIds = await _mediator.Send(new GetFollowersIdQuery { UserId = userId });

    var unreadNotificationCount =
        await _mediator.Send(new GetUnreadNotificationNumberQuery { UserId = userId });
    // start all async tasks
    // var activityIdsTask = _mediator.Send(new GetUserParticipatedActivitiesQuery
    // {
    //     UserId = userId
    // });
    //
    // var followingIdsTask = _mediator.Send(new GetFollowingIdQuery
    // {
    //     UserId = userId
    // });
    //
    // var unreadNotificationCountTask = _mediator.Send(new GetUnreadNotificationNumberQuery
    // {
    //     UserId = userId
    // });
    //
    // // wait for all async tasks to complete
    // await Task.WhenAll(activityIdsTask, followingIdsTask, unreadNotificationCountTask);
    //
    // // get all async tasks results
    // var activityIds = await activityIdsTask;
    // var followingIds = await followingIdsTask;
    // var unreadNotificationCount = await unreadNotificationCountTask;

    // add all async tasks results to the hub context

    // add user to his personal group, which will be used to send notifications to him
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

    await Clients.Caller.SendAsync("LoadUnreadNotificationNumber",
                                   unreadNotificationCount);
  }

  public async override Task OnDisconnectedAsync(Exception exception)
  {
    _connectionManager.RemoveConnection(Context.ConnectionId);
    await base.OnDisconnectedAsync(exception);
  }

  public async Task ReadNotification(string notificationId)
  {
    var userId = _currentUserService.Id!;

    var httpContext = Context.GetHttpContext();
    var userNotificationId = httpContext!.Request.Query["userNotificationId"];

    GuardValidation.AgainstNull(userNotificationId,
                                "notification with id cannot be null");

    // update notification status to read
    await _mediator.Send(new UpdateNotificationStatusCommand
    {
        UserNotificationId = userNotificationId!
    });

    // get new unread notification count
    var unreadNotificationCount = await _mediator.Send(new
                                                           GetUnreadNotificationNumberQuery
                                                           {
                                                               UserId = userId
                                                           });

    await Clients.Caller.SendAsync("UpdateUnreadNotificationNumber",
                                   unreadNotificationCount);
  }
}
