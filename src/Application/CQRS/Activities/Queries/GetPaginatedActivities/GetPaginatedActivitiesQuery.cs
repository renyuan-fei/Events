using Application.common.DTO;
using Application.Common.Helpers;
using Application.common.Interfaces;
using Application.Common.Interfaces;
using Application.common.Mappings;
using Application.common.Models;

using AutoMapper;
using AutoMapper.Internal;

using Domain.Constant;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.Queries.GetPaginatedActivities;

public record
    GetPaginatedActivitiesQuery : IRequest<PaginatedList<ActivityWithHostUserDto>>
{
  public PaginatedListParams PaginatedListParams { get; init; }

  public FilterParams? FilterParams { get; init; }
}

public class
    GetPaginatedActivitiesQueryHandler : IRequestHandler<GetPaginatedActivitiesQuery,
    PaginatedList<ActivityWithHostUserDto>>
{
  private readonly IPhotoRepository                            _photoRepository;
  private readonly IUserService                                _userService;
  private readonly IActivityRepository                         _activityRepository;
  private readonly IMapper                                     _mapper;
  private readonly ILogger<GetPaginatedActivitiesQueryHandler> _logger;

  public GetPaginatedActivitiesQueryHandler(
      IMapper                                     mapper,
      ILogger<GetPaginatedActivitiesQueryHandler> logger,
      IActivityRepository                         activityRepository,
      IUserService                                userService,
      IPhotoRepository                            photoRepository)
  {
    _mapper = mapper;
    _logger = logger;
    _activityRepository = activityRepository;
    _userService = userService;
    _photoRepository = photoRepository;
  }

public async Task<PaginatedList<ActivityWithHostUserDto>> Handle(
    GetPaginatedActivitiesQuery request,
    CancellationToken cancellationToken)
{
    try
    {
        var query = _activityRepository.GetAllActivitiesWithAttendeesQueryable();
        query = ApplyFilters(query, request.FilterParams);

        var pageNumber = request.PaginatedListParams.PageNumber;
        var pageSize = request.PaginatedListParams.PageSize;

        var paginatedActivitiesDto = await query
            .ProjectTo<ActivityWithHostUserDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(pageNumber, pageSize);

        if (!paginatedActivitiesDto.Items.Any())
        {
            return paginatedActivitiesDto;
        }

        var userIds = paginatedActivitiesDto.Items.Select(a => a.HostUser.Id).Distinct
            ().ToList();
        var activityIds = paginatedActivitiesDto.Items.Select(a => a.Id).ToList();

        GuardValidation.AgainstNullOrEmpty(userIds, "Error occurred while getting user ids");
        GuardValidation.AgainstNullOrEmpty(activityIds, "Error occurred while getting activity ids");

        var users = await _userService.GetUsersByIdsAsync(userIds, cancellationToken);
        var activityPhotos = await _photoRepository.GetMainPhotosByOwnerIdAsync(activityIds, cancellationToken);

        var usersDictionary = users.ToDictionary(user => user.Id);
        var activityPhotosDictionary = activityPhotos.ToDictionary(photo => photo.OwnerId);

        paginatedActivitiesDto.Items.ForAll(activity =>
        {
            var hostUser = activity.HostUser;
            if (usersDictionary.TryGetValue(hostUser.Id, out var user))
            {
                hostUser.Username = user.DisplayName;
            }
            activity.ImageUrl = activityPhotosDictionary.GetValueOrDefault(activity.Id)?.Details.Url ?? DefaultImage.DefaultActivityImageUrl;
        });

        return paginatedActivitiesDto;
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error occurred in {Name}: {ExMessage}", nameof(GetPaginatedActivitiesQuery), ex.Message);
        throw;
    }
}

  private static IQueryable<Activity> ApplyFilters(
      IQueryable<Activity> query,
      FilterParams?        filterParams)
  {
    if (filterParams == null) return query;

    Category? categoryEnum = null;

    if (!string.IsNullOrWhiteSpace(filterParams.Category))
    {
      Enum.TryParse<Category>(filterParams.Category, out var parsedCategory);
      categoryEnum = parsedCategory;
    }

    return query
           .Where(activity => string.IsNullOrWhiteSpace(filterParams.Title)
                           || activity.Title.Contains(filterParams.Title))
           .Where(activity => categoryEnum == null || activity.Category == categoryEnum)
           .Where(activity => string.IsNullOrWhiteSpace(filterParams.City)
                           || activity.Location.City.Contains(filterParams.City))
           .Where(activity => string.IsNullOrWhiteSpace(filterParams.Venue)
                           || activity.Location.Venue.Contains(filterParams.Venue))
           .Where(activity => !filterParams.StartDate.HasValue
                           || activity.Date >= filterParams.StartDate.Value)
           .Where(activity => !filterParams.EndDate.HasValue
                           || activity.Date <= filterParams.EndDate.Value);
  }
}
