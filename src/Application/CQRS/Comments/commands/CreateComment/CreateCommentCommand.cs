using Application.common.DTO;
using Application.common.Interfaces;
using Application.Common.Interfaces;

using AutoMapper;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;
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
  private readonly IMapper                              _mapper;
  private readonly ILogger<CreateCommentCommandHandler> _logger;
  private readonly IUserService                         _userService;

  public CreateCommentCommandHandler(
      IEventsDbContext                     context,
      IMapper                              mapper,
      ILogger<CreateCommentCommandHandler> logger,
      IUserService                         userService)
  {
    _context = context;
    _mapper = mapper;
    _logger = logger;
    _userService = userService;
  }

  public async Task<CommentDTO> Handle(
      CreateCommentCommand request,
      CancellationToken    cancellationToken)
  {
    throw new NotImplementedException();
  }
}
