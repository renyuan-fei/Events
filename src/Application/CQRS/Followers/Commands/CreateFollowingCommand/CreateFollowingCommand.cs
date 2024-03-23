using Application.Common.Helpers;
using Application.common.Interfaces;
using Application.Common.Interfaces;
using Application.common.Models;

using AutoMapper;

using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Followers.Commands.CreateFollowingCommand;

public record CreateFollowingCommand : IRequest<Result>
{
  public string UserId { get; init; }
  public string TargetUserId { get; init; }
}

public class CreateFollowingCommandHandler : IRequestHandler<CreateFollowingCommand, Result>
{
  private readonly IUserService                           _userService;
  private readonly IUnitOfWork                            _unitOfWork;
  private readonly IFollowingRepository                   _followingRepository;
  private readonly ILogger<CreateFollowingCommandHandler> _logger;

  public CreateFollowingCommandHandler(IUserService userService, IUnitOfWork unitOfWork, IFollowingRepository followingRepository, ILogger<CreateFollowingCommandHandler> logger)
  {
    _userService = userService;
    _unitOfWork = unitOfWork;
    _followingRepository = followingRepository;
    _logger = logger;
  }

  public async Task<Result> Handle(
      CreateFollowingCommand request,
      CancellationToken      cancellationToken)
  {
    try
    {
      var userId = new UserId(request.UserId);
      var targetUserId = new UserId(request.TargetUserId);

      var follower = await _followingRepository.IsFollowingAsync(userId, targetUserId);

      if (follower != null)
      {
        throw new InvalidOperationException("Cannot repeat follow");
      }

      var newFollowing = Follow.Create(userId, targetUserId);
      await _followingRepository.AddAsync(newFollowing, cancellationToken);

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
                       nameof(CreateFollowingCommand),
                       ex.Message);

      throw;
    }
  }
}

