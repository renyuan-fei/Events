using Application.Common.Interfaces;
using Application.common.Models;

using AutoMapper;

using Domain.Entities;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Comments.commands.DeleteComment;

public record DeleteCommentCommand : IRequest<Result>
{
  public string CommentId { get; init; }
  public string UserId { get; init; }
}

public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, Result>
{
  private readonly IMapper                              _mapper;
  private readonly ILogger<DeleteCommentCommandHandler> _logger;

  public DeleteCommentCommandHandler(
      IMapper                              mapper,
      ILogger<DeleteCommentCommandHandler> logger)
  {
    _mapper = mapper;
    _logger = logger;
  }

  public async Task<Result> Handle(
      DeleteCommentCommand request,
      CancellationToken    cancellationToken)
  {
    try { throw new NotImplementedException(); }
    catch (Exception ex)
    {
      _logger.LogError(ex,
                       "Error occurred in {Name}: {ExMessage}",
                       nameof(DeleteCommentCommand),
                       ex.Message);

      throw;
    }
  }
}

