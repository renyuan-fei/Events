using Application.common.DTO;
using Application.Common.Helpers;
using Application.common.Interfaces;

using Domain.Repositories;
using Domain.ValueObjects.Activity;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.Queries.GetActivityWithAttendees;

[ BypassAuthorization ]
public record GetActivityWithAttendeesByIdQuery : IRequest<ActivityWithAttendeeDTO>
{
  public string Id { get; init; }
}

public class
    GetActivityByIdQueryHandler : IRequestHandler<GetActivityWithAttendeesByIdQuery,
    ActivityWithAttendeeDTO>
{
  private readonly IActivityRepository                  _activityRepository;
  private readonly ILogger<GetActivityByIdQueryHandler> _logger;
  private readonly IMapper                              _mapper;
  private readonly IPhotoRepository                     _photoRepository;
  private readonly IUserService                         _userService;

  public GetActivityByIdQueryHandler(
      IMapper                              mapper,
      ILogger<GetActivityByIdQueryHandler> logger,
      IActivityRepository                  activityRepository,
      IPhotoRepository                     photoRepository,
      IUserService                         userService)
  {
    _mapper = mapper;
    _logger = logger;
    _activityRepository = activityRepository;
    _photoRepository = photoRepository;
    _userService = userService;
  }

  public async Task<ActivityWithAttendeeDTO> Handle(
      GetActivityWithAttendeesByIdQuery request,
      CancellationToken                 cancellationToken)
  {
    try
    {
      var activityId = new ActivityId(request.Id);
      var activity = await _activityRepository.GetActivityWithAttendeesByIdAsync(activityId, cancellationToken);

      GuardValidation.AgainstNull(activity, $"Activity with Id {request.Id} not found");

      if (!activity!.Attendees.Any())
      {
        _logger.LogInformation("No attendees found for activity with Id {ActivityId}", request.Id);
        return new ActivityWithAttendeeDTO();
      }

      var userIds = activity.Attendees.Select(a => a.Identity.UserId.Value).ToList();
      var usersTask = _userService.GetUsersByIdsAsync(userIds);
      var photosTask = _photoRepository.GetMainPhotosByOwnerIdAsync(userIds, cancellationToken);

      await Task.WhenAll(usersTask, photosTask);

      GuardValidation.AgainstNullOrEmpty(usersTask.Result, "User information for attendees not found");

      var usersDictionary = usersTask.Result.ToDictionary(u => u.Id);
      var photosDictionary = photosTask.Result.ToDictionary(p => p.OwnerId);

      var result = _mapper.Map<ActivityWithAttendeeDTO>(activity);
      return ActivityHelper.FillWithPhotoAndUserDetail(result, usersDictionary, photosDictionary);
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error occurred in {Name}: {ExMessage}", nameof(GetActivityByIdQueryHandler), ex
          .Message);
      throw;
    }
  }
}
