using Application.common.DTO;
using Application.Common.Interfaces;
using Application.common.Mappings;
using Application.common.Models;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.ActivityAttendee.Queries.GetActivityAttendeesWithPagination;

public record
    GetActivityAttendeesWithPaginationQuery : IRequest<
    PaginatedList<ActivityAttendeeDTO>>
{
  public Guid ActivityId { get; init; }

  public PaginatedListParams PaginatedListParams { get; init; }
}

public class
    GetActivityAttendeesWithPaginationQueryHandler : IRequestHandler<
    GetActivityAttendeesWithPaginationQuery,
    PaginatedList<ActivityAttendeeDTO>>
{
  private readonly IApplicationDbContext                                   _context;
  private readonly ILogger<GetActivityAttendeesWithPaginationQueryHandler> _logger;
  private readonly IMapper                                                 _mapper;

  public GetActivityAttendeesWithPaginationQueryHandler(
      IApplicationDbContext                                   context,
      IMapper                                                 mapper,
      ILogger<GetActivityAttendeesWithPaginationQueryHandler> logger)
  {
    _context = context;
    _mapper = mapper;
    _logger = logger;
  }

  public async Task<PaginatedList<ActivityAttendeeDTO>> Handle(
      GetActivityAttendeesWithPaginationQuery request,
      CancellationToken                       cancellationToken)
  {
    var query = _context.ActivityAttendees.Where(x => x.Id == request.ActivityId);

    if (!query.Any())
    {
      _logger.LogError("Could not find any attendee with Activity Id {Id}",
                       request.ActivityId);

      throw new NotFoundException(nameof(ActivityAttendeeDTO), request.ActivityId);
    }

    var result = await query.OrderBy(attendee => attendee.LastModified)
                            .ProjectTo<ActivityAttendeeDTO>(_mapper.ConfigurationProvider)
                            .PaginatedListAsync(request.PaginatedListParams.PageNumber,
                                                request.PaginatedListParams.PageSize);

    return result;
  }
}
