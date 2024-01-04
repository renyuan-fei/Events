using Application.common.DTO;
using Application.common.Helpers;
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
      var activityTask =
          _activityRepository
              .GetActivityWithAttendeesByIdAsync(new ActivityId(request.Id),
                                                 cancellationToken);

      var userIdsTask =
          activityTask.ContinueWith(t => t.Result?.Attendees
                                          .Select(a => a.Identity.UserId.Value)
                                          .ToList(),
                                    TaskContinuationOptions.OnlyOnRanToCompletion);

      await Task.WhenAll(activityTask, userIdsTask);

      var activityWithAttendees = activityTask.Result;

      Guard.Against.Null(activityWithAttendees,
                         $"Activity with Id {request.Id} not found");

      var userIds = userIdsTask.Result;

      Guard.Against.Null(userIds, message: "Could not get user ids");

      var usersTask = _userService.GetUsersByIdsAsync(userIds);

      var mainPhotosTask =
          _photoRepository
              .GetMainPhotosByUserIdAsync(userIds.Select(id => new UserId(id)),
                                          cancellationToken);

      await Task.WhenAll(usersTask, mainPhotosTask);

      var usersDictionary = usersTask.Result.ToDictionary(u => u.Id, u => u);
      ;

      var photosDictionary =
          mainPhotosTask.Result.ToDictionary(p => p.UserId.Value, p => p);

      var result = _mapper.Map<ActivityWithAttendeeDTO>(activityWithAttendees);

      result = ActivityHelper.FillWithPhotoAndUserDetail(result,
                                                         usersDictionary,
                                                         photosDictionary);

      return result;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "ErrorMessage Mapping to DTO: {ExMessage}", ex.Message);

      throw;
    }
  }
}
