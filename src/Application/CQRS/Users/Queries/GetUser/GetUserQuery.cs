using Application.common.DTO;
using Application.Common.Helpers;
using Application.common.Interfaces;

using AutoMapper.Internal;

using Domain.Constant;
using Domain.Repositories;
using Domain.ValueObjects;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Users.Queries.GetUser;

// Used for bypassing authorization behavior checks.
[ BypassAuthorization ]
public record GetUserQuery : IRequest<UserProfileDto>
{
  public string UserId { get; init; }
}

public class GetUserHandler : IRequestHandler<GetUserQuery, UserProfileDto>
{
  private readonly IFollowingRepository    _followingRepository;
  private readonly IActivityRepository     _activityRepository;
  private readonly IUserService            _userService;
  private readonly IPhotoRepository        _photoRepository;
  private readonly ILogger<GetUserHandler> _logger;
  private readonly IMapper                 _mapper;

  public GetUserHandler(
      IMapper                 mapper,
      ILogger<GetUserHandler> logger,
      IPhotoRepository        photoRepository,
      IUserService            userService,
      IFollowingRepository    followingRepository,
      IActivityRepository     activityRepository)
  {
    _mapper = mapper;
    _logger = logger;
    _photoRepository = photoRepository;
    _userService = userService;
    _followingRepository = followingRepository;
    _activityRepository = activityRepository;
  }

  public async Task<UserProfileDto> Handle(
      GetUserQuery      request,
      CancellationToken cancellationToken)
  {
    try
    {
      var user = await _userService.GetUserByIdAsync(request.UserId, cancellationToken);
      GuardValidation.AgainstNull(nameof(user), "User with Id {UserId} not found", request.UserId);
      var userMainPhoto = await _photoRepository.GetMainPhotoByOwnerIdAsync(user.Id,cancellationToken);

      var activities =
          await _activityRepository.GetActivityByUserIdAsync(new UserId(request.UserId),
            cancellationToken);

      var result = _mapper.Map<UserProfileDto>(user);
      // var userPhotos = _mapper.Map<List<PhotoDTO>>(photos);
      // result.Photos = userPhotos.Where(photo => photo.IsMain != true).ToList();
      var mainPhoto = userMainPhoto;

      result.Image = mainPhoto == null
          ? DefaultImage.DefaultUserImageUrl
          : mainPhoto.Details.Url;


      var followingQuery = _followingRepository.GetFollowingsByIdQueryable(new UserId(user
          .Id));
      var followersQuery = _followingRepository.GetFollowersByIdQueryable(new UserId(user
        .Id));

      result.Followers = followersQuery.Count();
      result.Following = followingQuery.Count();

      return result;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex,
                       "Error occurred in {Name}: {ExMessage}",
                       nameof(GetUserQuery),
                       ex.Message);

      throw;
    }
  }
}
