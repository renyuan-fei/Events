using Application.common.Constant;
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
      // 使用用户信息填充参与者信息
      if (usersDictionary.TryGetValue(attendee.UserId, out var user))
      {
        attendee.DisplayName = user.DisplayName;
        attendee.UserName = user.UserName;
        attendee.Bio = user.Bio;

        activity.HostUsername = attendee.IsHost
            ? user.UserName
            : string.Empty;
      }

      // 使用照片信息填充参与者的图片URL
      attendee.Image = photosDictionary.TryGetValue(attendee.UserId, out var photo)
          ? photo.Details.Url
          : DefaultImage.DefaultImageUrl;
    }

    return activity;
  }
}
