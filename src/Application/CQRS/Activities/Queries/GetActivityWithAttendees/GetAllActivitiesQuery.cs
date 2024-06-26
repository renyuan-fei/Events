using Application.common.DTO;
using Application.Common.Interfaces;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.Queries.GetActivity;

[ BypassAuthorization ]
public record GetAllActivitiesQuery : IRequest<List<ActivityWithAttendeeDto>>;

public class
    GetAllActivitiesQueryHandler : IRequestHandler<GetAllActivitiesQuery,
    List<ActivityWithAttendeeDto>>
{
  private readonly IEventsDbContext                      _context;
  private readonly ILogger<GetAllActivitiesQueryHandler> _logger;
  private readonly IMapper                               _mapper;

  public GetAllActivitiesQueryHandler(
      IMapper                               mapper,
      ILogger<GetAllActivitiesQueryHandler> logger,
      IMediator                             mediator,
      IEventsDbContext                      context)
  {
    _mapper = mapper;
    _logger = logger;
    _context = context;
  }

  public async Task<List<ActivityWithAttendeeDto>> Handle(
      GetAllActivitiesQuery request,
      CancellationToken     cancellationToken)
  {
    throw new NotImplementedException();
  }
}
