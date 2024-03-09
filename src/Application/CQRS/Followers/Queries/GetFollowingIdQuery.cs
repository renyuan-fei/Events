using Application.Common.Interfaces;

using AutoMapper;

using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Followers.Queries;

public record GetFollowingIdQuery : IRequest<List<string>>
{
  public string UserId { get; init; }
}

public class
    GetFollowingIdQueryHandler : IRequestHandler<GetFollowingIdQuery, List<string>>
{
  private readonly IFollowingRepository                _followingRepository;
  private readonly IMapper                             _mapper;
  private readonly ILogger<GetFollowingIdQueryHandler> _logger;

  public GetFollowingIdQueryHandler(
      IMapper                             mapper,
      ILogger<GetFollowingIdQueryHandler> logger,
      IFollowingRepository                followingRepository)
  {
    _mapper = mapper;
    _logger = logger;
    _followingRepository = followingRepository;
  }

  public async Task<List<string>> Handle(
      GetFollowingIdQuery request,
      CancellationToken   cancellationToken)
  {
    try
    {
      var followingId =
          await _followingRepository
              .GetFollowingsByIdQueryable(new UserId(request.UserId))
              .Select(following => following.Relationship.FollowingId.Value)
              .ToListAsync(cancellationToken: cancellationToken);

      return followingId;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex,
                       "Error occurred in {Name}: {ExMessage}",
                       nameof(GetFollowingIdQuery),
                       ex.Message);

      throw;
    }
  }
}
