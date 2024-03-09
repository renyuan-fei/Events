using Application.Common.Helpers;
using Application.Common.Interfaces;

using AutoMapper;

using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using Domain.ValueObjects.Activity;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.Queries.GetActivityHostIdQuery;

public record GetActivityHostIdQuery : IRequest<UserId>
{
  public ActivityId activityId { get; init; }
}

public class GetActivityHostIdQueryHandler : IRequestHandler<GetActivityHostIdQuery, UserId>
{
  private readonly IActivityRepository                    _activityRepository;
  private readonly IMapper                                _mapper;
  private readonly ILogger<GetActivityHostIdQueryHandler> _logger;

  public GetActivityHostIdQueryHandler(
      IMapper                                mapper,
      ILogger<GetActivityHostIdQueryHandler> logger,
      IActivityRepository                    activityRepository)
  {
    _mapper = mapper;
    _logger = logger;
    _activityRepository = activityRepository;
  }

  public async Task<UserId> Handle(
      GetActivityHostIdQuery request,
      CancellationToken      cancellationToken)
  {
    try
    {
      var activityId = request.activityId;

      var hostId =  await _activityRepository.GetHostIdAsync(activityId);

      GuardValidation.AgainstNull(hostId, "Host not found");

      return hostId!;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex,
                       "Error occurred in {Name}: {ExMessage}",
                       nameof(GetActivityHostIdQuery),
                       ex.Message);

      throw;
    }
  }
}

