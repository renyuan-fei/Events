using Application.common.DTO;

using AutoMapper;

using Infrastructure.Identity;

namespace Infrastructure.Mapping;

public class MappingProfile : Profile
{
  public MappingProfile()
  {
    CreateMap<UpdateUserDto, ApplicationUser>();

    CreateMap<ApplicationUser, UserDto>();

    CreateMap<ApplicationUser, UserProfileDto>();

    CreateMap<ApplicationUser, FollowingDTO>();
  }
}
