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

      var followingsTask = _userService.GetUsersByIdsAsync(followingsId);

      var mainPhotoTask = _photoRepository.GetMainPhotosByUserIdAsync(followingsId
            .Select(userId => new UserId(userId)),
        cancellationToken);

      var userDictionary =
          followingsTask.Result.ToDictionary(following => following.Id,
                                             following => following);

      var photosDictionary =
          mainPhotoTask.Result.ToDictionary(photo => photo.UserId.Value, photo => photo);

      foreach (var following in paginatedFollowingDto.Items)
      {
        // 使用用户信息填充参与者信息
        if (userDictionary.TryGetValue(following.UserId, out var user))
        {
          following.DisplayName = user.DisplayName;
          following.UserName = user.UserName;
          following.Bio = user.Bio;
        }

        // 使用照片信息填充参与者的图片URL
        following.Image = photosDictionary.TryGetValue(following.UserId, out var photo)
            ? photo.Details.Url
            : DefaultImage.DefaultImageUrl;
      }

      return paginatedFollowingDto;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error saving to the database: {ExMessage}", ex.Message);

      throw;
    }
  }
}
