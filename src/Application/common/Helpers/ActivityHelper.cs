using Application.common.DTO;

using Domain.Entities;

namespace Application.common.Helpers;

public static class ActivityHelper
{
  public static ActivityWithAttendeeDTO FillWithPhotoAndUserDetail(
      ActivityWithAttendeeDTO              activity,
      IReadOnlyDictionary<string, UserDTO> usersDictionary,
      IReadOnlyDictionary<string, Photo>   photosDictionary)
  {
    // 遍历活动的每个参与者
    foreach (var attendee in activity.Attendees)
    {
      UserHelper.FillWithPhotoAndUserDetail(
       attendee, usersDictionary, photosDictionary);

      if (attendee.IsHost)
      {
        activity.HostUsername = attendee.UserName;
      }

    }

    return activity;
  }
}
