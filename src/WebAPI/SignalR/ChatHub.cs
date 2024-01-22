using Application.common.interfaces;
using Application.CQRS.Comments.commands.CreateComment;
using Application.CQRS.Comments.Queries.GetComments;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace WebAPI.SignalR;

/// <summary>
///   A SignalR hub that handles chat functionality.
/// </summary>
public class ChatHub : Hub
{
  /// <summary>
  ///   Represents a mediator used for handling communication between components.
  /// </summary>
  private readonly IMediator _mediator;
  private readonly ICurrentUserService _currentUserService;

  /// <summary>
  ///   Initializes a new instance of the ChatHub class.
  /// </summary>
  /// <param name="mediator">
  ///   An instance of the IMediator interface to handle communication
  ///   with the mediator.
  /// </param>
  /// /
  public ChatHub(IMediator mediator, ICurrentUserService currentUserService)
  {
    _mediator = mediator;
    _currentUserService = currentUserService;
  }

  /// <summary>
  ///   Sends a comment and notifies the group about the new comment.
  /// </summary>
  /// <returns>A Task representing the asynchronous operation.</returns>
  public async Task SendComment(string body)
  {
    var httpContext = Context.GetHttpContext();
    var activityId = httpContext!.Request.Query["activityId"];

    var comment =
        await _mediator.Send(new CreateCommentCommand
        {
            Body = body, ActivityId = activityId!, UserId = _currentUserService.Id!
        });

    await Clients.Group(activityId!)
                 .SendAsync("ReceiveComment", comment);
  }

  /// <summary>
  ///   Method called when a client connects to the hub.
  /// </summary>
  /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
  [ Authorize ]
  public async override Task OnConnectedAsync()
  {
    var httpContext = Context.GetHttpContext();
    var activityId = httpContext!.Request.Query["activityId"];

    await Groups.AddToGroupAsync(Context.ConnectionId, activityId!);

    var result = await _mediator.Send(new GetCommentsQuery
    {
        ActivityId = activityId!
    });

    await Clients.Caller.SendAsync("LoadComments", result);
  }
}
