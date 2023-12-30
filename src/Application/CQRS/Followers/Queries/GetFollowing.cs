using Application.common.DTO;
using Application.common.Interfaces;
using Application.Common.Interfaces;
using Application.common.Models;

using AutoMapper;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Followers.Queries;

public record GetFollowing : IRequest<List<FollowingDTO>>
{
  public bool IsFollowing { get; init; }
  public Guid UserId      { get; init; }
}

public class GetPaginatedFolloweeHandler : IRequestHandler<GetFollowing,
    List<FollowingDTO>>
{
  private readonly IEventsDbContext                     _context;
  private readonly IMapper                              _mapper;
  private readonly ILogger<GetPaginatedFolloweeHandler> _logger;

  public GetPaginatedFolloweeHandler(
      IEventsDbContext                     context,
      IMapper                              mapper,
      ILogger<GetPaginatedFolloweeHandler> logger)
  {
    _context = context;
    _mapper = mapper;
    _logger = logger;
  }

  public async Task<List<FollowingDTO>> Handle(
      GetFollowing      request,
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
