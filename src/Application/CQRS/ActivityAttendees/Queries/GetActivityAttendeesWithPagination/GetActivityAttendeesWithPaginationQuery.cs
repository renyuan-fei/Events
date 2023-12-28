using Application.common.DTO;
using Application.Common.Interfaces;
using Application.common.Mappings;
using Application.common.Models;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.ActivityAttendees.Queries.GetActivityAttendeesWithPagination;

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

  public async Task<PaginatedList<ActivityAttendeeDTO>> Handle(
      GetActivityAttendeesWithPaginationQuery request,
      CancellationToken                       cancellationToken)
  {
    throw new NotImplementedException();
  }
}
