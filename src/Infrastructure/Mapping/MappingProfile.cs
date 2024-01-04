using Application.common.DTO;

using AutoMapper;

using Infrastructure.Identity;

namespace Infrastructure.Mapping;

public class MappingProfile : Profile
{
  public MappingProfile()
  {
    CreateMap<UpdateUserDTO, ApplicationUser>();

    CreateMap<ApplicationUser, UserDTO>();

    CreateMap<ApplicationUser, UserProfileDTO>();

    CreateMap<ApplicationUser, FollowingDTO>();
  }
}
