using Application.Common.Interfaces;

using AutoMapper;

using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Followers.Queries;

public record GetFollowersIdQuery : IRequest<List<string>>
{
  public string UserId { get; init; }
}

public class
    GetFollowersIdQueryHandler : IRequestHandler<GetFollowersIdQuery, List<string>>
{
  private readonly IFollowingRepository                _followingRepository;
  private readonly IMapper                             _mapper;
  private readonly ILogger<GetFollowersIdQueryHandler> _logger;

  public GetFollowersIdQueryHandler(
      IMapper                             mapper,
      ILogger<GetFollowersIdQueryHandler> logger,
      IFollowingRepository                followingRepository)
  {
    _mapper = mapper;
    _logger = logger;
    _followingRepository = followingRepository;
  }

  public async Task<List<string>> Handle(
      GetFollowersIdQuery request,
      CancellationToken   cancellationToken)
  {
    try
    {
      var followerIds =
          await _followingRepository
              .GetFollowersByIdQueryable(new UserId(request.UserId))
              .Select(following => following.Relationship.FollowingId.Value)
              .ToListAsync(cancellationToken: cancellationToken);

      return followerIds;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex,
                       "Error occurred in {Name}: {ExMessage}",
                       nameof(GetFollowersIdQuery),
                       ex.Message);

      throw;
    }
  }
}
