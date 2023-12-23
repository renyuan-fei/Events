using Application.common.Interfaces;
using Application.Common.Interfaces;

using AutoMapper;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Followers.Commands.UpdateFollowingCommand;

public record UpdateFollowerCommand : IRequest<Unit>
{
  public Guid UserId     { get; init; }
  public Guid FolloweeId { get; init; }
}

public class CreateFollowerCommandHandler : IRequestHandler<UpdateFollowerCommand, Unit>
{
  private readonly IEventsDbContext                      _context;
  private readonly IMapper                               _mapper;
  private readonly ILogger<CreateFollowerCommandHandler> _logger;
  private readonly IUserService                          _userService;

  public CreateFollowerCommandHandler(
      IEventsDbContext                      context,
      IMapper                               mapper,
      ILogger<CreateFollowerCommandHandler> logger,
      IUserService                          userService)
  {
    _context = context;
    _mapper = mapper;
    _logger = logger;
    _userService = userService;
  }

  public async Task<Unit> Handle(
      UpdateFollowerCommand request,
      CancellationToken     cancellationToken)
  {
    try
    {
      var following =
          await _context.UserFollowings.FirstOrDefaultAsync(x =>
                                                                  x.FollowerId
                                                               == request.UserId
                                                               && x.FolloweeId
                                                               == request.FolloweeId,
                                                              cancellationToken:
                                                              cancellationToken);

      if (following != null)
      {
        _context.UserFollowings.Remove(following);

        _logger.LogInformation("Following removed");
      }
      else
      {
        var isFollowerExists = await _userService.IsUserExistsAsync(request.UserId);
        var isFolloweeExists = await _userService.IsUserExistsAsync(request.FolloweeId);

        if (!isFollowerExists)
        {
          throw new NotFoundException($"Observer {request.UserId} not found.");
        }

        if (!isFolloweeExists)
        {
          throw new NotFoundException($"Target {request.FolloweeId} not found.");
        }

        var newFollowing = new UserFollowing
        {
            FolloweeId = request.FolloweeId, FollowerId = request.UserId
        };

        _context.UserFollowings.Add(newFollowing);
      }

      var result = await _context.SaveChangesAsync(cancellationToken: cancellationToken)
                 > 0;

      return result
          ? Unit.Value
          : throw new DbUpdateException();
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error saving to the database: {ExMessage}", ex.Message);

      throw;
    }
  }
}
