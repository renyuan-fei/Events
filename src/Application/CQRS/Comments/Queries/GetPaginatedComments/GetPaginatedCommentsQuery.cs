using Application.common.DTO;
using Application.Common.Interfaces;
using Application.common.Models;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Comments.Queries.GetPaginatedComments;

public record GetPaginatedCommentsQuery : IRequest<PaginatedList<CommentDTO>>
{
  public PaginatedListParams PaginatedListParams { get; init; }
  public Guid                ActivityId          { get; init; }
}

public class
    GetPaginatedCommentsQueryHandler : IRequestHandler<GetPaginatedCommentsQuery,
    PaginatedList<CommentDTO>>
{
  private readonly IEventsDbContext                          _context;
  private readonly ILogger<GetPaginatedCommentsQueryHandler> _logger;
  private readonly IMapper                                   _mapper;

  public GetPaginatedCommentsQueryHandler(
      IEventsDbContext                          context,
      IMapper                                   mapper,
      ILogger<GetPaginatedCommentsQueryHandler> logger)
  {
    _context = context;
    _mapper = mapper;
    _logger = logger;
  }

  public async Task<PaginatedList<CommentDTO>> Handle(
      GetPaginatedCommentsQuery request,
      CancellationToken         cancellationToken)
  {
    try { throw new NotImplementedException(); }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error saving to the database: {ExMessage}", ex.Message);

      throw;
    }
  }
}
