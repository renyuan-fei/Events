using Application.common.DTO;
using Application.Common.Helpers;
using Application.common.Interfaces;

using Domain.Constant;
using Domain.Repositories;
using Domain.ValueObjects.Activity;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.Queries.GetActivityWithAttendees;

// TODO Do not return Photos in the response
[ BypassAuthorization ]
public record GetActivityWithAttendeesByIdQuery : IRequest<ActivityWithAttendeeDto>
{
  public string Id { get; init; }
}

public class
    GetActivityByIdQueryHandler : IRequestHandler<GetActivityWithAttendeesByIdQuery,
    ActivityWithAttendeeDto>
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
    this._userService = userService;
    _userService = userService;
  }

  public async Task<ActivityWithAttendeeDto> Handle(
      GetActivityWithAttendeesByIdQuery request,
      CancellationToken                 cancellationToken)
  {
    try
    {
      var activityId = new ActivityId(request.Id);

      var activity =
          await _activityRepository.GetActivityWithAttendeesByIdAsync(activityId,
            cancellationToken);

      GuardValidation.AgainstNull(activity, $"Activity with Id {request.Id} not found");

      if (!activity!.Attendees.Any())
      {
        _logger.LogInformation("No attendees found for activity with Id {ActivityId}",
                               request.Id);

        return new ActivityWithAttendeeDto();
      }

      var userIds = activity.Attendees.Select(a => a.Identity.UserId.Value).ToList();
      var users = await _userService.GetUsersByIdsAsync(userIds, cancellationToken);
      var photos = await _photoRepository.GetMainPhotosByOwnerIdAsync(userIds, cancellationToken);

      GuardValidation.AgainstNullOrEmpty(users, "User information for attendees not found");

      var usersDictionary = users.ToDictionary(u => u.Id);
      var photosDictionary = photos.ToDictionary(p => p.OwnerId);

      var activityPhotos =
          await _photoRepository.GetPhotosByOwnerIdAsync(activityId.Value,
                                                         cancellationToken);

      var result = _mapper.Map<ActivityWithAttendeeDto>(activity);

      var activityMainPhoto = activityPhotos.FirstOrDefault(p => p.Details.IsMain);

      if (activityMainPhoto == null)
      {
        result.ImageUrl = DefaultImage.DefaultActivityImageUrl;
      }
      else
      {
        result.ImageUrl = activityMainPhoto.Details.Url;

        result.Photos = activityPhotos.Where(p => !p.Details.IsMain)
                                      .Select(p => _mapper.Map<PhotoDto>(p))
                                      .ToList();
      }

      return ActivityHelper.FillWithPhotoAndUserDetail(result,
                                                       usersDictionary,
                                                       photosDictionary);
    }
    catch (Exception ex)
    {
      _logger.LogError(ex,
                       "Error occurred in {Name}: {ExMessage}",
                       nameof(GetActivityByIdQueryHandler),
                       ex
                           .Message);

      throw;
    }
  }
}
