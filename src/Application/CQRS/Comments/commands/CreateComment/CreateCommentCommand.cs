using Application.common.DTO;
using Application.Common.Interfaces;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Comments.commands.CreateComment;

public record CreateCommentCommand : IRequest<CommentDTO>
{
  public string Body       { get; init; } = null!;
  public Guid   ActivityId { get; init; }
}

public class
    CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CommentDTO>
{
  private readonly IEventsDbContext                     _context;
  private readonly ILogger<CreateCommentCommandHandler> _logger;
  private readonly IMapper                              _mapper;

  public CreateCommentCommandHandler(
      IEventsDbContext                     context,
      IMapper                              mapper,
      ILogger<CreateCommentCommandHandler> logger)
  {
    _context = context;
    _mapper = mapper;
    _logger = logger;
  }

  public async Task<CommentDTO> Handle(
      CreateCommentCommand request,
      CancellationToken    cancellationToken)
  {
    throw new NotImplementedException();
  }
}
