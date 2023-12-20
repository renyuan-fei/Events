using Application.CQRS.Comments.commands.CreateComment;
using Application.CQRS.Comments.Queries.GetPaginatedComments;

using MediatR;

using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.SignalR;

public class ChatHub : Hub
{
  private readonly IMediator _mediator;

  public ChatHub(IMediator mediator) { _mediator = mediator; }

  public async Task SendComment(CreateCommentCommand command)
  {
    var comment = await _mediator.Send(command);

    await Clients.Group(command.ActivityId.ToString())
                 .SendAsync("ReceiveComment", comment);
  }

  public async override Task OnConnectedAsync()
  {
    var httpContext = Context.GetHttpContext();
    var activityId = httpContext!.Request.Query["activityId"];

    await Groups.AddToGroupAsync(Context.ConnectionId, activityId);

    var result = await _mediator.Send(new GetPaginatedCommentsQuery
    {
        ActivityId = Guid.Parse(activityId!)
    });

    await Clients.Caller.SendAsync("LoadComments", result);
  }
}
