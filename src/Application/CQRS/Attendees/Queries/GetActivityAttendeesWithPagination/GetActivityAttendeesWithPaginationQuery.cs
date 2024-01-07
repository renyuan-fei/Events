using Application.common.DTO;
using Application.Common.Interfaces;
using Application.common.Models;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Attendees.Queries.GetActivityAttendeesWithPagination;

public record
    GetActivityAttendeesWithPaginationQuery : IRequest<
    PaginatedList<AttendeeDTO>>
{
  public Guid ActivityId { get; init; }

  public PaginatedListParams PaginatedListParams { get; init; }
}

public class
    GetActivityAttendeesWithPaginationQueryHandler : IRequestHandler<
    GetActivityAttendeesWithPaginationQuery,
    PaginatedList<AttendeeDTO>>
{
  private readonly IEventsDbContext                                        _context;
  private readonly ILogger<GetActivityAttendeesWithPaginationQueryHandler> _logger;
  private readonly IMapper                                                 _mapper;

  public GetActivityAttendeesWithPaginationQueryHandler(
      IEventsDbContext                                        context,
      IMapper                                                 mapper,
      ILogger<GetActivityAttendeesWithPaginationQueryHandler> logger)
  {
    _context = context;
    _mapper = mapper;
    _logger = logger;
  }

  public async Task<PaginatedList<AttendeeDTO>> Handle(
      GetActivityAttendeesWithPaginationQuery request,
      CancellationToken                       cancellationToken)
  {
    throw new NotImplementedException();
  }
}
