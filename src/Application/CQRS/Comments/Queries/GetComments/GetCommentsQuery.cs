using Application.common.DTO;
using Application.common.Interfaces;
using Application.Common.Interfaces;
using Application.common.Mappings;

using AutoMapper;

using Domain.Entities;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Comments.Queries.GetComments;

public record GetCommentsQuery : IRequest<List<CommentDTO>>
{
  public Guid ActivityId { get; init; }
}

public class GetCommentsQueryHandler : IRequestHandler<GetCommentsQuery, List<CommentDTO>>
{
  private readonly IEventsDbContext                 _context;
  private readonly IMapper                          _mapper;
  private readonly ILogger<GetCommentsQueryHandler> _logger;

  public GetCommentsQueryHandler(
      IEventsDbContext                 context,
      IMapper                          mapper,
      ILogger<GetCommentsQueryHandler> logger)
  {
    _context = context;
    _mapper = mapper;
    _logger = logger;
  }

  public async Task<List<CommentDTO>> Handle(
      GetCommentsQuery  request,
      CancellationToken cancellationToken)
  {
    try
    {
      throw new NotImplementedException();
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error saving to the database: {ExMessage}", ex.Message);

      throw;
    }
  }
}