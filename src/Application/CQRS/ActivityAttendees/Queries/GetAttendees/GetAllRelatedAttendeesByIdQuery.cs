using Application.common.DTO;
using Application.Common.Interfaces;
using Application.common.Mappings;

using AutoMapper;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.ActivityAttendee.Queries;

public record GetAllRelatedAttendeesByIdQuery : IRequest<List<ActivityAttendeeDTO>>
{
  public Guid ActivityId { get; init; }
}

public class
    GetAllRelatedAttendeesByIdQueryHandler : IRequestHandler<
    GetAllRelatedAttendeesByIdQuery, List<ActivityAttendeeDTO>>
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

  public async Task<List<ActivityAttendeeDTO>> Handle(
      GetAllRelatedAttendeesByIdQuery request,
      CancellationToken               cancellationToken)
  {
    var query = _context.ActivityAttendees.Where(x => x.ActivityId == request.ActivityId);

    if (!query.Any())
    {
      _logger.LogError("Could not find any attendee with Activity Id {Id}",
                       request.ActivityId);

      throw new NotFoundException(nameof(ActivityAttendeeDTO), request.ActivityId);
    }

    var result = await query.OrderBy(attendee => attendee.LastModified)
                            .ProjectToListAsync<
                                ActivityAttendeeDTO>(_mapper.ConfigurationProvider);

    return result;
  }
}
