using Application.common.DTO;

using AutoMapper;

using Infrastructure.Identity;

namespace Infrastructure.Mapping;

public class MappingProfile: Profile
{
  public MappingProfile()
  {
    CreateMap<UserDTO, ApplicationUser>();

    CreateMap<ApplicationUser, UserInfoDTO>();

    CreateMap<ApplicationUser, FollowingDTO>();
  }
}
