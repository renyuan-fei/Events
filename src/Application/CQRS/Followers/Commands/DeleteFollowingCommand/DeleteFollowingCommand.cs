using Application.common.Interfaces;
using Application.Common.Interfaces;
using Application.common.Models;

using AutoMapper;

using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Followers.Commands.DeleteFollowingCommand;

public record DeleteFollowingCommand : IRequest<Result>
{
  public string UserId       { get; init; }
  public string TargetUserId { get; init; }
}

public class DeleteFollowingCommandHandler : IRequestHandler<DeleteFollowingCommand, Result>
{
  private readonly IUserService                           _userService;
  private readonly IUnitOfWork                            _unitOfWork;
  private readonly IFollowingRepository                   _followingRepository;
  private readonly ILogger<DeleteFollowingCommandHandler> _logger;

  public DeleteFollowingCommandHandler(
      IUserService                           userService,
      IUnitOfWork                            unitOfWork,
      IFollowingRepository                   followingRepository,
      ILogger<DeleteFollowingCommandHandler> logger)
  {
    _userService = userService;
    _unitOfWork = unitOfWork;
    _followingRepository = followingRepository;
    _logger = logger;
  }

  public async Task<Result> Handle(
      DeleteFollowingCommand request,
      CancellationToken      cancellationToken)
  {
    try
    {
      var userId = new UserId(request.UserId);
      var targetUserId = new UserId(request.TargetUserId);

      var follower = await _followingRepository.IsFollowingAsync(userId, targetUserId);

      if (follower == null)
      {
        throw new InvalidOperationException("The user is not following the target user.");
      }

      _followingRepository.Remove(follower);

      var result = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

      if (!result)
      {
        throw new Exception("There was an error saving data to the database");
      }

      return Result.Success();
    }
    catch (Exception ex)
    {
      _logger.LogError(ex,
                       "Error occurred in {Name}: {ExMessage}",
                       nameof(DeleteFollowingCommand),
                       ex.Message);

      throw;
    }
  }
}
