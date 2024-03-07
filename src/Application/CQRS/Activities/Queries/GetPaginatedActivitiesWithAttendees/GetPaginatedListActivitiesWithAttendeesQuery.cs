using Application.common.DTO;
using Application.common.Interfaces;
using Application.common.Mappings;
using Application.common.Models;

using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.Queries.GetPaginatedActivitiesWithAttendees;

[ BypassAuthorization ]
public record GetPaginatedListActivitiesWithAttendeesQuery : IRequest<PaginatedList<ActivityWithAttendeeDto>>
{
  public PaginatedListParams PaginatedListParams { get; init; }
  public FilterParams?       FilterParams        { get; init; }
}

public class
    GetPaginatedListActivitiesQueryHandler :
    IRequestHandler<GetPaginatedListActivitiesWithAttendeesQuery,
    PaginatedList<ActivityWithAttendeeDto>>
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

  public async Task<PaginatedList<ActivityWithAttendeeDto>> Handle(
      GetPaginatedListActivitiesWithAttendeesQuery request,
      CancellationToken               cancellationToken)
  {
    try
    {
      var query = _activityRepository.GetAllActivitiesQueryable();
      query = ApplyFilters(query, request.FilterParams);

      var pageNumber = request.PaginatedListParams.PageNumber;
      var pageSize = request.PaginatedListParams.PageSize;

      // 直接进行分页处理
      var paginatedActivitiesDto = await query
                                         .ProjectTo<
                                             ActivityWithAttendeeDto>(_mapper
                                                 .ConfigurationProvider)
                                         .PaginatedListAsync(pageNumber, pageSize);

      // 如果查询结果为空，PaginatedListAsync 将返回包含空 Items 集合的 PaginatedList 对象
      if (!paginatedActivitiesDto.Items.Any())
      {
        return new PaginatedList<ActivityWithAttendeeDto>();
      }

      var userIds = paginatedActivitiesDto
                    .Items.SelectMany(activity =>
                                          activity.Attendees.Select(attendee =>
                                              attendee.UserId))
                    .Distinct()
                    .ToList();

      var usersTask = _userService.GetUsersByIdsAsync(userIds, cancellationToken);

      var mainPhotosTask =
          _photoRepository
              .GetMainPhotosByOwnerIdAsync(userIds.Select(id => id),
                                          cancellationToken);

      await Task.WhenAll(usersTask, mainPhotosTask);

      var usersDictionary = usersTask.Result.ToDictionary(user => user.Id, user => user);

      var photosDictionary =
          mainPhotosTask.Result.ToDictionary(photo => photo.OwnerId, photo => photo);

      // 重新创建分页结果
      return paginatedActivitiesDto.UpdateItems(activity =>
                                                    ActivityHelper
                                                        .FillWithPhotoAndUserDetail(activity,
                                                          usersDictionary,
                                                          photosDictionary));
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error occurred in {Name}: {ExMessage}", nameof(GetPaginatedListActivitiesWithAttendeesQuery), ex
                           .Message);
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
