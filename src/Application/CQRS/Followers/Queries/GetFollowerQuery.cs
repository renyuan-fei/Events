using Application.common.Constant;
using Application.common.DTO;
using Application.Common.Helpers;
using Application.common.Interfaces;
using Application.Common.Interfaces;
using Application.common.Mappings;
using Application.common.Models;

using AutoMapper;

using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Followers.Queries;

public record GetFollowerQuery : IRequest<PaginatedList<FollowerDTO>>
{
  public PaginatedListParams PaginatedListParams { get; init; }
  public string              UserId              { get; init; }
}

public class GetFollowerQueryHandler : IRequestHandler<GetFollowerQuery,
    PaginatedList<FollowerDTO>>
{
  private readonly IUserService                     _userService;
  private readonly IPhotoRepository                 _photoRepository;
  private readonly IFollowingRepository             _followingRepository;
  private readonly IMapper                          _mapper;
  private readonly ILogger<GetFollowerQueryHandler> _logger;

  public GetFollowerQueryHandler(
      IMapper                          mapper,
      ILogger<GetFollowerQueryHandler> logger,
      IPhotoRepository                 photoRepository,
      IUserService                     userService,
      IFollowingRepository             followingRepository)
  {
    _mapper = mapper;
    _logger = logger;
    _photoRepository = photoRepository;
    _userService = userService;
    _followingRepository = followingRepository;
  }

  public async Task<PaginatedList<FollowerDTO>> Handle(
      GetFollowerQuery  request,
      CancellationToken cancellationToken)
  {
    try
    {
      var id = new UserId(request.UserId);
      var pageNumber = request.PaginatedListParams.PageNumber;
      var pageSize = request.PaginatedListParams.PageSize;

      var query = _followingRepository.GetFollowersByIdQueryable(id);
      var paginatedFollowerDto = await query
                                       .ProjectTo<FollowerDTO>(_mapper.ConfigurationProvider)
                                       .PaginatedListAsync(pageNumber, pageSize);

      if (!paginatedFollowerDto.Items.Any())
      {
        return new PaginatedList<FollowerDTO>();
      }


      var followingsId =
          paginatedFollowerDto.Items.Select(following => following.UserId).ToList();

      var followingsTask = _userService.GetUsersByIdsAsync(followingsId);

      var mainPhotoTask = _photoRepository.GetMainPhotosByUserIdAsync(followingsId
            .Select(userId => new UserId(userId)),
        cancellationToken);

      var userDictionary =
          followingsTask.Result.ToDictionary(following => following.Id,
                                             following => following);

      var photosDictionary =
          mainPhotoTask.Result.ToDictionary(photo => photo.UserId.Value, photo => photo);

      foreach (var following in paginatedFollowerDto.Items)
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

      return paginatedFollowerDto;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex,
                       "Error occurred in {Name}: {ExMessage}",
                       nameof(GetFollowerQuery),
                       ex.Message);

      throw;
    }
  }
}
