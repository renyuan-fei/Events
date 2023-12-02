using Application.common.Exceptions;
using Application.Common.Interfaces;
using Application.common.Mappings;
using Application.common.Models;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Domain.Entities;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.Queries.GetActivity;

public record GetPaginatedListActivitiesQuery : IRequest<PaginatedList<Activity>>
{
  public PaginatedListParams PaginatedListParams { get; init; }
}

internal sealed class
    GetPaginatedListActivitiesQueryHandler :
        IRequestHandler<GetPaginatedListActivitiesQuery,
        PaginatedList<Activity>>
{
  private readonly IApplicationDbContext                           _context;
  private readonly IMapper                                         _mapper;
  private readonly ILogger<GetPaginatedListActivitiesQueryHandler> _logger;

  public GetPaginatedListActivitiesQueryHandler(
      IApplicationDbContext                           context,
      IMapper                                         mapper,
      ILogger<GetPaginatedListActivitiesQueryHandler> logger)
  {
    _context = context;
    _mapper = mapper;
    _logger = logger;
  }

  public async Task<PaginatedList<Activity>> Handle(
      GetPaginatedListActivitiesQuery request,
      CancellationToken               cancellationToken)
  {
    var query = _context.Activities.AsQueryable();

    if (!string.IsNullOrWhiteSpace(request.PaginatedListParams.SearchTerm))
    {
      var searchTerm = request.PaginatedListParams.SearchTerm.ToLower().Trim();

      query = query.Where(activity =>
                              (activity.Title.ToLower().Contains(searchTerm))
                           || (activity.Category.ToLower().Contains(searchTerm))
                           || (activity.Description.ToLower().Contains(searchTerm))
                           || (activity.City.ToLower().Contains(searchTerm))
                           || (activity.Venue.ToLower().Contains(searchTerm)));
    }

    // empty no found
    if (!query.Any())
    {
      _logger.LogError("Could not find activities with search term {SearchTerm}",
                       request.PaginatedListParams.SearchTerm);

      throw new NotFoundException(nameof(Activity),
                                  request.PaginatedListParams.SearchTerm);
    }

    var result = await query
                       .OrderBy(activity => activity.Title) // 或者其他排序方式
                       .ProjectTo<
                           Activity>(_mapper.ConfigurationProvider) // 如果需要映射到 DTO
                       .PaginatedListAsync(request.PaginatedListParams.PageNumber,
                                           request.PaginatedListParams.PageSize);

    return result;
  }
}
