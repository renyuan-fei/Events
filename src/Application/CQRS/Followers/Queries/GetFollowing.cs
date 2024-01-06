using Application.common.Constant;
using Application.common.DTO;
using Application.Common.Helpers;
using Application.common.Interfaces;
using Application.Common.Interfaces;
using Application.common.Mappings;
using Application.common.Models;

using Domain.Repositories;
using Domain.ValueObjects;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Followers.Queries;

public record GetFollowing : IRequest<PaginatedList<FollowingDTO>>
{
  public PaginatedListParams PaginatedListParams { get; init; }
  public string UserId      { get; init; }
}

public class GetPaginatedFolloweeHandler : IRequestHandler<GetFollowing,
    PaginatedList<FollowingDTO>>
{
  private readonly IUserService                         _userService;
  private readonly IPhotoRepository                     _photoRepository;
  private readonly IFollowingRepository                 _followingRepository;
  private readonly ILogger<GetPaginatedFolloweeHandler> _logger;
  private readonly IMapper                              _mapper;

  public GetPaginatedFolloweeHandler(
      IMapper                              mapper,
      ILogger<GetPaginatedFolloweeHandler> logger,
      IFollowingRepository                 followingRepository,
      IPhotoRepository                     photoRepository,
      IUserService                         userService)
  {
    _mapper = mapper;
    _logger = logger;
    _followingRepository = followingRepository;
    _photoRepository = photoRepository;
    _userService = userService;
  }

  public async Task<PaginatedList<FollowingDTO>> Handle(
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
