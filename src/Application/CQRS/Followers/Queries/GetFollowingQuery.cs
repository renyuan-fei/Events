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

public record GetFollowingQuery : IRequest<PaginatedList<FollowingDTO>>
{
  public PaginatedListParams PaginatedListParams { get; init; }
  public string              UserId              { get; init; }
}

public class GetPaginatedFollowingHandler : IRequestHandler<GetFollowingQuery,
    PaginatedList<FollowingDTO>>
{
  private readonly IUserService                          _userService;
  private readonly IPhotoRepository                      _photoRepository;
  private readonly IFollowingRepository                  _followingRepository;
  private readonly ILogger<GetPaginatedFollowingHandler> _logger;
  private readonly IMapper                               _mapper;

  public GetPaginatedFollowingHandler(
      IMapper                               mapper,
      ILogger<GetPaginatedFollowingHandler> logger,
      IFollowingRepository                  followingRepository,
      IPhotoRepository                      photoRepository,
      IUserService                          userService)
  {
    _mapper = mapper;
    _logger = logger;
    _followingRepository = followingRepository;
    _photoRepository = photoRepository;
    _userService = userService;
  }

  public async Task<PaginatedList<FollowingDTO>> Handle(
      GetFollowingQuery request,
      CancellationToken cancellationToken)
  {
    try
    {
      var id = new UserId(request.UserId);
      var pageNumber = request.PaginatedListParams.PageNumber;
      var pageSize = request.PaginatedListParams.PageSize;

      var query = _followingRepository.GetFollowingsByIdQueryable(id);
      var paginatedFollowingDto = await query
                                        .ProjectTo<FollowingDTO>(_mapper.ConfigurationProvider)
                                        .PaginatedListAsync(pageNumber, pageSize);

      if (!paginatedFollowingDto.Items.Any())
      {
        return new PaginatedList<FollowingDTO>();
      }


      var followingsId =
          paginatedFollowingDto.Items.Select(following => following.UserId).ToList();

      var followingsTask = _userService.GetUsersByIdsAsync(followingsId, cancellationToken);

      var mainPhotoTask = _photoRepository.GetMainPhotosByOwnerIdAsync(followingsId
            .Select(userId => userId),
        cancellationToken);

      var userDictionary =
          followingsTask.Result.ToDictionary(following => following.Id,
                                             following => following);

      var photosDictionary =
          mainPhotoTask.Result.ToDictionary(photo => photo.OwnerId, photo => photo);

      return paginatedFollowingDto.UpdateItems(follower => UserHelper
                                                   .FillWithPhotoAndUserDetail(follower, userDictionary, photosDictionary));
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error saving to the database: {ExMessage}", ex.Message);

      throw;
    }
  }
}
