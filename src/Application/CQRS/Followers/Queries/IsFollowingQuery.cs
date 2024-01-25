using Application.common.Interfaces;
using Application.Common.Interfaces;

using AutoMapper;

using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Followers.Queries;

public record IsFollowingQuery : IRequest<bool>
{
  public string UserId { get; init; }
  public string targetUserId { get; init; }
}

public class IsFollowingQueryHandler : IRequestHandler<IsFollowingQuery, bool>
{
  private readonly IFollowingRepository             _followingRepository;
  private readonly IUserService                     _userService;
  private readonly ILogger<IsFollowingQueryHandler> _logger;

  public IsFollowingQueryHandler(
      ILogger<IsFollowingQueryHandler> logger,
      IUserService                     userService,
      IFollowingRepository             followingRepository)
  {
    _logger = logger;
    _userService = userService;
    _followingRepository = followingRepository;
  }

  public async Task<bool> Handle(
      IsFollowingQuery  request,
      CancellationToken cancellationToken)
  {
    try
    {
      var userId = new UserId(request.UserId);
      var targetUserId = new UserId(request.targetUserId);

      var isFollowing = await _followingRepository.IsFollowingAsync(userId,
          targetUserId) != null;

      return isFollowing;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex,
                       "Error occurred in {Name}: {ExMessage}",
                       nameof(IsFollowingQuery),
                       ex.Message);

      throw;
    }
  }
}
