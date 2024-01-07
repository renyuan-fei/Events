using Application.common.Constant;
using Application.common.DTO;
using Application.common.DTO.Interface;

using Domain.Entities;

namespace Application.common.Helpers;

public static class UserHelper
{
  public static T FillWithPhotoAndUserDetail <T>(
      T                                    data,
      IReadOnlyDictionary<string, UserDTO> usersDictionary,
      IReadOnlyDictionary<string, Photo>   photosDictionary)
  where T : IUserDetail
  {
    if (usersDictionary.TryGetValue(data.UserId, out var user))
    {
      data.DisplayName = user.DisplayName;
      data.UserName = user.UserName;
      data.Bio = user.Bio;
    }

    // 使用照片信息填充参与者的图片URL
    data.Image = photosDictionary.TryGetValue(data.UserId, out var photo)
        ? photo.Details.Url
        : DefaultImage.DefaultImageUrl;

    return data;
  }
}
