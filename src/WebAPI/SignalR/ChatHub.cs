using Application.common.interfaces;
using Application.common.Models;
using Application.CQRS.Comments.commands.CreateComment;
using Application.CQRS.Comments.commands.DeleteComment;
using Application.CQRS.Comments.Queries.GetComments;
using Application.CQRS.Comments.Queries.GetPaginatedComments;

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
  private readonly ILogger<ChatHub> _logger;

  private readonly IMediator           _mediator;
  private readonly ICurrentUserService _currentUserService;

  /// <summary>
  ///   Initializes a new instance of the ChatHub class.
  /// </summary>
  /// <param name="mediator">
  ///   An instance of the IMediator interface to handle communication
  ///   with the mediator.
  /// </param>
  /// <param name="currentUserService"></param>
  /// <param name="logger"></param>
  /// /
  public ChatHub(
      IMediator           mediator,
      ICurrentUserService currentUserService,
      ILogger<ChatHub>    logger)
  {
    _mediator = mediator;
    _currentUserService = currentUserService;
    _logger = logger;
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
            Body = body,
            ActivityId = activityId!,
            UserId = _currentUserService.Id!
        });

    await Clients.Group(activityId!)
                 .SendAsync("ReceiveComment", comment);
  }

  /// <summary>
  /// Deletes a comment and notifies the group about the deleted comment.
  /// </summary>
  /// <param name="commentId">The ID of the comment to delete.</param>
  public async Task DeleteComment(string commentId)
  {
    var httpContext = Context.GetHttpContext();
    var activityId = httpContext!.Request.Query["activityId"];

    await _mediator.Send(new DeleteCommentCommand
    {
        CommentId = commentId, UserId = _currentUserService.Id!
    });

    await Clients.Group(activityId!)
                 .SendAsync("DeleteComment", commentId);
  }

  public async Task LoadPaginatedComments(
      int      pageNumber,
      int      pageSize,
      DateTime initialTimestamp)
  {
    var httpContext = Context.GetHttpContext();
    var activityId = httpContext!.Request.Query["activityId"];

    var result = await _mediator.Send(new GetPaginatedCommentsQuery
    {
        ActivityId = activityId!,
        PaginatedListParams =
            new PaginatedListParams
            {
                InitialTimestamp = initialTimestamp,
                PageNumber = pageNumber,
                PageSize = pageSize,
            }
    });

    await Clients.Caller.SendAsync("LoadComments", result);
  }

  /// <summary>
  ///   Method called when a client connects to the hub.
  /// </summary>
  /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
  public async override Task OnConnectedAsync()
  {
    var httpContext = Context.GetHttpContext();
    var activityId = httpContext!.Request.Query["activityId"];

    await Groups.AddToGroupAsync(Context.ConnectionId, activityId!);

    var result = await _mediator.Send(new GetPaginatedCommentsQuery
    {
        ActivityId = activityId!,
        PaginatedListParams =
            new PaginatedListParams
            {
                InitialTimestamp = DateTime.MaxValue, PageNumber = 1, PageSize = 15,
            }
    });

    await Clients.Caller.SendAsync("LoadComments", result);
  }
}
