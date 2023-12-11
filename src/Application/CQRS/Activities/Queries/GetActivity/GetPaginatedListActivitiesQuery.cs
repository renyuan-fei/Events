using Application.common.DTO;
using Application.Common.Interfaces;
using Application.common.Mappings;
using Application.common.Models;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.Queries.GetActivity;

public record GetPaginatedListActivitiesQuery : IRequest<PaginatedList<ActivityDTO>>
{
  public PaginatedListParams PaginatedListParams { get; init; }
}

public class
    GetPaginatedListActivitiesQueryHandler :
    IRequestHandler<GetPaginatedListActivitiesQuery,
    PaginatedList<ActivityDTO>>
{
  private readonly IEventsDbContext                                _context;
  private readonly ILogger<GetPaginatedListActivitiesQueryHandler> _logger;
  private readonly IMapper                                         _mapper;

  public GetPaginatedListActivitiesQueryHandler(
      IMapper                                         mapper,
      ILogger<GetPaginatedListActivitiesQueryHandler> logger,
      IEventsDbContext                                context)
  {
    _mapper = mapper;
    _logger = logger;
    _context = context;
  }

  public async Task<PaginatedList<ActivityDTO>> Handle(
      GetPaginatedListActivitiesQuery request,
      CancellationToken               cancellationToken)
  {
    var query = _context.Activities.AsQueryable();

    if (!string.IsNullOrWhiteSpace(request.PaginatedListParams.SearchTerm))
    {
      var searchTerm = request.PaginatedListParams.SearchTerm.ToLower().Trim();

      query = query.Where(activity =>
                              activity.Title.ToLower().Contains(searchTerm)
                           || activity.Category.ToLower().Contains(searchTerm)
                           || activity.Description.ToLower().Contains(searchTerm)
                           || activity.City.ToLower().Contains(searchTerm)
                           || activity.Venue.ToLower().Contains(searchTerm));
    }

    // empty no found
    if (!query.Any())
    {
      _logger.LogError("Could not find activities with search term {SearchTerm}",
                       request.PaginatedListParams.SearchTerm);

      throw new NotFoundException(nameof(ActivityDTO),
                                  request.PaginatedListParams.SearchTerm!);
    }

    var result = await query
                       .OrderBy(activity => activity.Title) // 或者其他排序方式
                       .ProjectTo<
                           ActivityDTO>(_mapper.ConfigurationProvider) // 如果需要映射到 DTO
                       .PaginatedListAsync(request.PaginatedListParams.PageNumber,
                                           request.PaginatedListParams.PageSize);

    return result;
  }
}
