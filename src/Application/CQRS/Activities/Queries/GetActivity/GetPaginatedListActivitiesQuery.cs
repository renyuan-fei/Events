using Application.common.DTO;
using Application.common.Helpers;
using Application.Common.Helpers;
using Application.common.Interfaces;
using Application.common.Mappings;
using Application.common.Models;

using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using Domain.ValueObjects;

using Microsoft.Extensions.Logging;

using static System.Enum;

namespace Application.CQRS.Activities.Queries.GetActivity;

[ BypassAuthorization ]
public record
    GetPaginatedListActivitiesQuery : IRequest<PaginatedList<ActivityWithAttendeeDTO>>
{
  public PaginatedListParams PaginatedListParams { get; init; }
  public FilterParams?       FilterParams        { get; init; }
}

public class
    GetPaginatedListActivitiesQueryHandler :
    IRequestHandler<GetPaginatedListActivitiesQuery,
    PaginatedList<ActivityWithAttendeeDTO>>
{
  private readonly IActivityRepository                             _activityRepository;
  private readonly ILogger<GetPaginatedListActivitiesQueryHandler> _logger;
  private readonly IMapper                                         _mapper;
  private readonly IPhotoRepository                                _photoRepository;
  private readonly IUserService                                    _userService;

  public GetPaginatedListActivitiesQueryHandler(
      IMapper                                         mapper,
      ILogger<GetPaginatedListActivitiesQueryHandler> logger,
      IActivityRepository                             activityRepository,
      IUserService                                    userService,
      IPhotoRepository                                photoRepository)
  {
    _mapper = mapper;
    _logger = logger;
    _activityRepository = activityRepository;
    _userService = userService;
    _photoRepository = photoRepository;
  }

  public async Task<PaginatedList<ActivityWithAttendeeDTO>> Handle(
      GetPaginatedListActivitiesQuery request,
      CancellationToken               cancellationToken)
  {
    try
    {
      var query = _activityRepository.GetAllActivitiesWithAttendeesQueryable();
      query = ApplyFilters(query, request.FilterParams);

      var pageNumber = request.PaginatedListParams.PageNumber;
      var pageSize = request.PaginatedListParams.PageSize;

      // 直接进行分页处理
      var paginatedActivitiesDto = await query
                                         .ProjectTo<
                                             ActivityWithAttendeeDTO>(_mapper
                                                 .ConfigurationProvider)
                                         .PaginatedListAsync(pageNumber, pageSize);

      // 如果查询结果为空，PaginatedListAsync 将返回包含空 Items 集合的 PaginatedList 对象
      if (!paginatedActivitiesDto.Items.Any())
      {
        return new PaginatedList<ActivityWithAttendeeDTO>();
      }

      var userIds = paginatedActivitiesDto
                    .Items.SelectMany(activity =>
                                          activity.Attendees.Select(attendee =>
                                              attendee.UserId))
                    .Distinct()
                    .ToList();

      var usersTask = _userService.GetUsersByIdsAsync(userIds);

      var mainPhotosTask =
          _photoRepository
              .GetMainPhotosByUserIdAsync(userIds.Select(id => new UserId(id)),
                                          cancellationToken);

      await Task.WhenAll(usersTask, mainPhotosTask);

      var usersDictionary = usersTask.Result.ToDictionary(user => user.Id, user => user);

      var photosDictionary =
          mainPhotosTask.Result.ToDictionary(photo => photo.UserId.Value, photo => photo);

      // 重新创建分页结果
      return paginatedActivitiesDto.UpdateItems(activity =>
                                                    ActivityHelper
                                                        .FillWithPhotoAndUserDetail(activity,
                                                          usersDictionary,
                                                          photosDictionary));
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "ErrorMessage Mapping to DTO: {ExMessage}", ex.Message);

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
      TryParse<Category>(filterParams.Category, out var parsedCategory);
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
