using Application.common.Interfaces;
using Application.Common.Interfaces;
using Application.common.Models;

using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Followers.Commands.UpdateFollowingCommand;

public record UpdateFollowerCommand : IRequest<Result>
{
  public string FollowingId { get; init; }
  public string FollowerId  { get; init; }
}

public class CreateFollowerCommandHandler : IRequestHandler<UpdateFollowerCommand, Result>
{
  private readonly IUserService                          _userService;
  private readonly IUnitOfWork                           _unitOfWork;
  private readonly IFollowingRepository                  _followingRepository;
  private readonly ILogger<CreateFollowerCommandHandler> _logger;

  public CreateFollowerCommandHandler(
      ILogger<CreateFollowerCommandHandler> logger,
      IFollowingRepository                  followingRepository,
      IUnitOfWork                           unitOfWork,
      IUserService                          userService)
  {
    _logger = logger;
    _followingRepository = followingRepository;
    _unitOfWork = unitOfWork;
    _userService = userService;
  }

  public async Task<Result> Handle(
      UpdateFollowerCommand request,
      CancellationToken     cancellationToken)
  {
    try
    {
      var IsFollowingExists = await _userService.GetUserByIdAsync(request.FollowingId);



      var followerId = new UserId(request.FollowerId);
      var followingId = new UserId(request.FollowingId);

      var follower =
          await _followingRepository.IsFollowingAsync(followerId, followingId);

      if (follower == null)
      {
        var newFollowing = Following.Create(followerId, followingId);

        await _followingRepository.AddAsync(newFollowing, cancellationToken);
      }
      else
      {
        _followingRepository.Remove(follower);
        follower.Unfollow();
      }

      var result = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

      return result
          ? Result.Success()
          : Result.Failure(new[ ] { "Failed to update follower" });
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error saving to the database: {ExMessage}", ex.Message);

      throw;
    }
  }
}
