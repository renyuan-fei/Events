using Application.common.Constant;
using Application.common.DTO;
using Application.common.Helpers;
using Application.common.Interfaces;
using Application.Common.Interfaces;
using Application.common.Mappings;
using Application.common.Models;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using Domain.ValueObjects;

using MediatR;

using Microsoft.EntityFrameworkCore;
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
  private readonly IUserService                                    _userService;
  private readonly IPhotoRepository                                _photoRepository;
  private readonly IActivityRepository                             _activityRepository;
  private readonly ILogger<GetPaginatedListActivitiesQueryHandler> _logger;
  private readonly IMapper                                         _mapper;

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

      var paginatedActivitiesDto = await query.ProjectTo<ActivityWithAttendeeDTO>(_mapper.ConfigurationProvider)
                                              .PaginatedListAsync(request.PaginatedListParams.PageNumber, request.PaginatedListParams.PageSize);

      Guard.Against.NullOrEmpty(paginatedActivitiesDto.Items);

      var userIds = paginatedActivitiesDto
                    .Items.SelectMany(activity => activity.Attendees.Select(attendee => attendee.UserId))
                    .Distinct()
                    .ToList();

      var usersTask = _userService.GetUsersByIdsAsync(userIds);

      var mainPhotosTask = _photoRepository.GetMainPhotosByUserIdAsync(userIds.Select(id => new UserId(id)),
                                          cancellationToken);

      await Task.WhenAll(usersTask, mainPhotosTask);

      var usersDictionary = usersTask.Result.ToDictionary(user => user.Id, user => user);
      var photosDictionary = mainPhotosTask.Result.ToDictionary(photo => photo.UserId.Value, photo => photo);

      // 填充每个活动的参与者信息
      var filledActivities = paginatedActivitiesDto.Items.Select(activity =>
          ActivityHelper.FillWithPhotoAndUserDetail(activity, usersDictionary, photosDictionary))
          .ToList();

      // 重新创建分页结果
      return new PaginatedList<ActivityWithAttendeeDTO>(
                                                        filledActivities, paginatedActivitiesDto.TotalCount,
                                                        request.PaginatedListParams.PageNumber, request.PaginatedListParams.PageSize);
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
           .Where(activity => string.IsNullOrWhiteSpace(filterParams.Title) ||
                              activity.Title.Contains(filterParams.Title))
           .Where(activity => categoryEnum == null ||
                              activity.Category == categoryEnum)
           .Where(activity => string.IsNullOrWhiteSpace(filterParams.City) ||
                              activity.Location.City.Contains(filterParams.City))
           .Where(activity => string.IsNullOrWhiteSpace(filterParams.Venue) ||
                              activity.Location.Venue.Contains(filterParams.Venue))
           .Where(activity => !filterParams.StartDate.HasValue ||
                              activity.Date >= filterParams.StartDate.Value)
           .Where(activity => !filterParams.EndDate.HasValue ||
                              activity.Date <= filterParams.EndDate.Value);
  }
}
