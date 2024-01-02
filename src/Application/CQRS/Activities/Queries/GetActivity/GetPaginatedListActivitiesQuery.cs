using Application.common.DTO;
using Application.common.Interfaces;
using Application.Common.Interfaces;
using Application.common.Mappings;
using Application.common.Models;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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

  public GetPaginatedListActivitiesQueryHandler(
      IMapper                                         mapper,
      ILogger<GetPaginatedListActivitiesQueryHandler> logger,
      IActivityRepository                             activityRepository)
  {
    _mapper = mapper;
    _logger = logger;
    _activityRepository = activityRepository;
  }

  public async Task<PaginatedList<ActivityWithAttendeeDTO>> Handle(
      GetPaginatedListActivitiesQuery request,
      CancellationToken               cancellationToken)
  {
    try
    {
      var query = _activityRepository.GetAllActivitiesWithAttendeesQueryable();

      // 添加基于新属性的过滤条件
      if (!string.IsNullOrWhiteSpace(request.FilterParams.Title))
      {
        query = query.Where(activity =>
                                activity.Title.Contains(request.FilterParams.Title));
      }

      if (!string.IsNullOrWhiteSpace(request.FilterParams.Category))
      {
        if (Enum.TryParse<Category>(request.FilterParams.Category, out var categoryEnum))
        {
          query = query.Where(activity => activity.Category == categoryEnum);
        }
      }

      if (!string.IsNullOrWhiteSpace(request.FilterParams.City))
      {
        query = query.Where(activity =>
                                activity.Location.City
                                        .Contains(request.FilterParams.City));
      }

      if (!string.IsNullOrWhiteSpace(request.FilterParams.Venue))
      {
        query = query.Where(activity =>
                                activity.Location.Venue.Contains(request.FilterParams
                                    .Venue));
      }

      if (request.FilterParams.StartDate.HasValue)
      {
        query = query.Where(activity =>
                                activity.Date
                             >= request.FilterParams.StartDate.Value);
      }

      if (request.FilterParams.EndDate.HasValue)
      {
        query = query.Where(activity =>
                                activity.Date <= request.FilterParams.EndDate.Value);
      }

      var paginatedActivities = await PaginatedList<Activity>.CreateAsync(query,
        request.PaginatedListParams.PageNumber,
        request.PaginatedListParams.PageSize);

      Guard.Against.NullOrEmpty(paginatedActivities.Items);

      return _mapper.Map<PaginatedList<ActivityWithAttendeeDTO>>(paginatedActivities);
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "ErrorMessage Mapping to DTO: {ExMessage}", ex.Message);

      throw;
    }
  }
}
