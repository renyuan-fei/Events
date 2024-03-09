using Application.common.interfaces;
using Application.CQRS.Activities.Queries.GetUserParticipatedActivitiesQuery;
using Application.CQRS.Followers.Queries;
using Application.CQRS.Notifications.Commands;
using Application.CQRS.Notifications.Queries;

using Microsoft.AspNetCore.SignalR;

namespace WebAPI.SignalR;

public class NotificationHub : Hub
{
  private readonly IMediator           _mediator;
  private readonly ICurrentUserService _currentUserService;

  public NotificationHub(IMediator mediator, ICurrentUserService currentUserService)
  {
    _mediator = mediator;
    _currentUserService = currentUserService;
  }

  public async override Task OnConnectedAsync()
  {
    var userId = _currentUserService.Id!;

    // start all async tasks
    var activityIdsTask = _mediator.Send(new GetUserParticipatedActivitiesQuery
    {
        UserId = userId
    });

    var followingIdsTask = _mediator.Send(new GetFollowingIdQuery
    {
        UserId = userId
    });

    var unreadNotificationCountTask = _mediator.Send(new GetUnreadNotificationNumberQuery
    {
        UserId = userId
    });

    // wait for all async tasks to complete
    await Task.WhenAll(activityIdsTask, followingIdsTask, unreadNotificationCountTask);

    // get all async tasks results
    var activityIds = await activityIdsTask;
    var followingIds = await followingIdsTask;
    var unreadNotificationCount = await unreadNotificationCountTask;

    // add all async tasks results to the hub context

    // add user to his personal group, which will be used to send notifications to him
    await Groups.AddToGroupAsync(Context.ConnectionId, $"user-{userId}");

    foreach (var activityId in activityIds)
    {
      await Groups.AddToGroupAsync(Context.ConnectionId, activityId);
    }

    foreach (var followingId in followingIds)
    {
      await Groups.AddToGroupAsync(Context.ConnectionId, followingId);
    }

    await Clients.Caller.SendAsync("LoadUnreadNotificationNumber",
                                   unreadNotificationCount);
  }

  public async Task ReadNotification(string notificationId)
  {
    var userId = _currentUserService.Id!;

    var httpContext = Context.GetHttpContext();
    var userNotificationId = httpContext!.Request.Query["userNotificationId"];

    // update notification status to read
    await _mediator.Send(new UpdateNotificationStatusCommand
    {
        UserNotificationId = userNotificationId,
    });

    // get new unread notification count
    var unreadNotificationCount = await _mediator.Send(new
        GetUnreadNotificationNumberQuery
        {
            UserId = userId
        });

    await Clients.Caller.SendAsync("UpdateUnreadNotificationNumber",unreadNotificationCount);
  }
}
