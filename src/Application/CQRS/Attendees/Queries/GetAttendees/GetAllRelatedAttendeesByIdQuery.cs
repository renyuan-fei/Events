using Application.common.DTO;
using Application.Common.Interfaces;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.ActivityAttendee.Queries;

public record GetAllRelatedAttendeesByIdQuery : IRequest<List<AttendeeDto>>
{
  public Guid ActivityId { get; init; }
}

public class
    GetAllRelatedAttendeesByIdQueryHandler : IRequestHandler<
    GetAllRelatedAttendeesByIdQuery, List<AttendeeDto>>
{
  private readonly IEventsDbContext                                _context;
  private readonly ILogger<GetAllRelatedAttendeesByIdQueryHandler> _logger;
  private readonly IMapper                                         _mapper;

  public GetAllRelatedAttendeesByIdQueryHandler(
      IEventsDbContext                                context,
      IMapper                                         mapper,
      ILogger<GetAllRelatedAttendeesByIdQueryHandler> logger)
  {
    _context = context;
    _mapper = mapper;
    _logger = logger;
  }

  public async Task<List<AttendeeDto>> Handle(
      GetAllRelatedAttendeesByIdQuery request,
      CancellationToken               cancellationToken)
  {
    throw new NotImplementedException();
  }
}
