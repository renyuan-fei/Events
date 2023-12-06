using Application.common.DTO;
using Application.Common.Interfaces;
using Application.common.Models;

using AutoMapper;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.ActivityAttendee.Queries.GetActivityAttendeesWithPagination;

public record
    GetActivityAttendeesWithPaginationQuery : IRequest<
        PaginatedList<ActivityAttendeeDTO>>
{
  //TODO
}

public class
    GetActivityAttendeesWithPaginationQueryHandler : IRequestHandler<
        GetActivityAttendeesWithPaginationQuery,
        PaginatedList<ActivityAttendeeDTO>>
{
  private readonly IApplicationDbContext                                   _context;
  private readonly IMapper                                                 _mapper;
  private readonly ILogger<GetActivityAttendeesWithPaginationQueryHandler> _logger;

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
    throw new NotImplementedException();
  }
}
