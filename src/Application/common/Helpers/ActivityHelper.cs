using Application.common.DTO;
using Domain.Constant;
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
      UserHelper.FillWithPhotoAndUserDetail(attendee, usersDictionary, photosDictionary);

      if (!attendee.IsHost) continue;

      // 设置活动主持人的ID和用户名，这两个步骤与图片URL无关，因此提前执行
      activity.HostUser.Id = attendee.UserId;
      activity.HostUser.Username = attendee.UserName!;

      // 根据条件设置图片URL
      var imageUrl = DefaultImage.DefaultUserImageUrl; // 默认图片URL
      if (photosDictionary.TryGetValue(attendee.UserId, out var photo) && photo.Details?.Url != null)
      {
        imageUrl = photo.Details.Url; // 如果找到相应的图片并且URL不为空，则使用该URL
      }

      // 将最终的图片URL赋值给活动主持人的ImageUrl属性
      activity.HostUser.ImageUrl = imageUrl;
    }

    return activity;
  }
}
