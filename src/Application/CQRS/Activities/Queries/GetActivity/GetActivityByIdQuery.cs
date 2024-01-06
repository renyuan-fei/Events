using Application.common.DTO;
using Application.common.Helpers;
using Application.Common.Helpers;
using Application.common.Interfaces;

using Domain.Repositories;
using Domain.ValueObjects;
using Domain.ValueObjects.Activity;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.Queries.GetActivity;

[ BypassAuthorization ]
public record GetActivityByIdQuery : IRequest<ActivityWithAttendeeDTO>
{
  public string Id { get; init; }
}

public class
    GetActivityByIdQueryHandler : IRequestHandler<GetActivityByIdQuery,
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
      GetActivityByIdQuery request,
      CancellationToken    cancellationToken)
  {
    try
    {
      var activity = await _activityRepository.GetActivityWithAttendeesByIdAsync(new ActivityId(request.Id), cancellationToken);

      GuardValidation.AgainstNull(activity, $"Activity with Id {request.Id} not found");

      var userIds = activity!.Attendees.Select(a => a.Identity.UserId.Value).ToList();

      GuardValidation.AgainstNullOrEmpty(userIds, "UserIds cannot be null or empty");

      var usersTask = _userService.GetUsersByIdsAsync(userIds);
      var mainPhotosTask = _photoRepository.GetMainPhotosByUserIdAsync(userIds.Select(id => new UserId(id)), cancellationToken);

      await Task.WhenAll(usersTask, mainPhotosTask);

      GuardValidation.AgainstNullOrEmpty(usersTask.Result, "Users cannot be null or empty");

      var usersDictionary = usersTask.Result.ToDictionary(u => u.Id, u => u);
      var photosDictionary = mainPhotosTask.Result.ToDictionary(p => p.UserId.Value, p => p);

      var result = _mapper.Map<ActivityWithAttendeeDTO>(activity);
      return ActivityHelper.FillWithPhotoAndUserDetail(result, usersDictionary, photosDictionary);

    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "ErrorMessage Mapping to DTO: {ExMessage}", ex.Message);

      throw;
    }
  }
}
