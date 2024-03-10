using Application.Common.Interfaces;

using AutoMapper;

using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using Domain.ValueObjects.Activity;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.Queries.GetAllAttendeeIdsByActivityIdQuery;

public record GetAllAttendeeIdsByActivityIdQuery : IRequest<List<UserId>>
{
  public ActivityId ActivityId { get; init; }
}

public class
    GetAllAttendeeIdsByActivityIdQueryHandler : IRequestHandler<
    GetAllAttendeeIdsByActivityIdQuery, List<UserId>>
{
  private readonly IActivityRepository                                _activityRepository;
  private readonly IMapper                                            _mapper;
  private readonly ILogger<GetAllAttendeeIdsByActivityIdQueryHandler> _logger;

  public GetAllAttendeeIdsByActivityIdQueryHandler(
      IMapper                                            mapper,
      ILogger<GetAllAttendeeIdsByActivityIdQueryHandler> logger,
      IActivityRepository                                activityRepository)
  {
    _mapper = mapper;
    _logger = logger;
    _activityRepository = activityRepository;
  }

  public async Task<List<UserId>> Handle(
      GetAllAttendeeIdsByActivityIdQuery request,
      CancellationToken                  cancellationToken)
  {
    try
    {
      var activityId = request.ActivityId;

      var userIds = await _activityRepository.GetAllAttendeeIdsByActivityIdAsync(activityId, cancellationToken);

      return userIds;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex,
                       "Error occurred in {Name}: {ExMessage}",
                       nameof(GetAllAttendeeIdsByActivityIdQuery),
                       ex.Message);

      throw;
    }
  }
}
