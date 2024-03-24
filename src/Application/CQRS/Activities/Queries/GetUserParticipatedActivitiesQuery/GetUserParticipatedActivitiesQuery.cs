using Domain.Repositories;
using Domain.ValueObjects;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.Queries.GetUserParticipatedActivitiesQuery;

public record GetUserParticipatedActivitiesQuery : IRequest<List<string>>
{
  public string UserId { get; init; }
}

public class
    GetUserParticipatedActivitiesQueryHandler : IRequestHandler<GetUserParticipatedActivitiesQuery, List<string>>
{
  private readonly IActivityRepository                    _activityRepository;
  private readonly IMapper                                _mapper;
  private readonly ILogger<GetUserParticipatedActivitiesQueryHandler> _logger;

  public GetUserParticipatedActivitiesQueryHandler(
      IMapper                                mapper,
      ILogger<GetUserParticipatedActivitiesQueryHandler> logger,
      IActivityRepository                    activityRepository)
  {
    _mapper = mapper;
    _logger = logger;
    _activityRepository = activityRepository;
  }

  public async Task<List<string>> Handle(
      GetUserParticipatedActivitiesQuery request,
      CancellationToken      cancellationToken)
  {
    try
    {
      var activityIds = await _activityRepository.GetAllActivitiesQueryable()
                                          .Where(activity => activity.Attendees.Any(attendee => attendee.Identity.UserId == new UserId(request.UserId)))
                                          .Where(activity => activity.Date >= DateTime.UtcNow)
                                          .Select(activity => activity.Id.Value)
                                          .ToListAsync(cancellationToken: cancellationToken);
      return activityIds;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex,
                       "Error occurred in {Name}: {ExMessage}",
                       nameof(GetUserParticipatedActivitiesQuery),
                       ex.Message);

      throw;
    }
  }
}