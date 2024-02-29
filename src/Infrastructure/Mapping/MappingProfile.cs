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

    CreateMap<ApplicationUser, FollowingDto>();

    CreateMap<UserDto, ApplicationUser>()
        .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
        .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
        .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
        .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.Bio))
        .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
        // Id 不进行映射，因为它通常在创建时由数据库生成
        .ForMember(dest => dest.Id, opt => opt.Ignore());

  }
}
