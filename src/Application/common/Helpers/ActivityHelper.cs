using Application.common.DTO;

using Domain.Entities;

namespace Application.common.Helpers;

public static class ActivityHelper
{
  public static ActivityWithAttendeeDto FillWithPhotoAndUserDetail(
      ActivityWithAttendeeDto              activity,
      IReadOnlyDictionary<string, UserDto> usersDictionary,
      IReadOnlyDictionary<string, Photo>   photosDictionary)
  {
    // 遍历活动的每个参与者
    foreach (var attendee in activity.Attendees)
    {
      UserHelper.FillWithPhotoAndUserDetail(
       attendee, usersDictionary, photosDictionary);

      if (!attendee.IsHost) continue;

      activity.HostUser.Id = attendee.UserId;
      activity.HostUser.Username = attendee.UserName!;
      activity.HostUser.ImageUrl = photosDictionary[attendee.UserId].Details.Url;
    }

    return activity;
  }
}
