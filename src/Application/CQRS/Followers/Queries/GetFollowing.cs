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
  private readonly IUserService                         _userService;

  public GetPaginatedFolloweeHandler(
      IEventsDbContext                     context,
      IMapper                              mapper,
      ILogger<GetPaginatedFolloweeHandler> logger,
      IUserService                         userService)
  {
    _context = context;
    _mapper = mapper;
    _logger = logger;
    _userService = userService;
  }

  public async Task<List<FollowingDTO>> Handle(
      GetFollowing      request,
      CancellationToken cancellationToken)
  {
    try
    {
      var usersId = _context.UserFollowings
                            .Where(x => (request.IsFollowing
                                            ? x.FollowerId
                                            : x.FolloweeId)
                                     == request
                                            .UserId)
                            .Distinct()
                            .Select(x => request.IsFollowing
                                        ? x.FollowerId
                                        : x.FolloweeId)
                            .ToList();

      var following = await _userService.GetUsersInfoByIdsAsync(usersId);

      return _mapper.Map<List<FollowingDTO>>(following);
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error saving to the database: {ExMessage}", ex.Message);

      throw;
    }
  }
}
