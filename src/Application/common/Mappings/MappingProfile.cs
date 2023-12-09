using Application.common.DTO;

using AutoMapper;

using Domain.Entities;

namespace Application.common.Mappings;

/// <summary>
///   The MappingProfile class is responsible for configuring object mappings using
///   AutoMapper.
/// </summary>
public class MappingProfile : Profile
{
  /// <summary>
  ///   AutoMapper profile for mapping entities to DTOs.
  /// </summary>
  public MappingProfile()
  {
    CreateMap<Activity, Activity>()
        .ForMember(activity => activity.Id, opt => opt.Ignore())
        .ForMember(activity => activity.Attendees, opt => opt.Ignore());

    CreateMap<Activity, ActivityDTO>()
        .ForMember(d => d.HostUsername,
                   o => o.MapFrom(s => s.Attendees
                                        .FirstOrDefault(x => x.IsHost)!.UserName));

    CreateMap<ActivityAttendee, ActivityAttendeeDTO>();
    CreateMap<ActivityAttendee, UserInfoDTO>();
  }
}
