using Application.common.DTO;

using AutoMapper;

using Domain.Entities;

namespace Application.common.Mappings;

public class MappingProfile : Profile
{
  public MappingProfile()
  {
    CreateMap<Activity, Activity>().ForMember(activity => activity.Id, opt => opt.Ignore());

    CreateMap<Activity, ActivityDTO>()
        .ForMember(d => d.HostUsername,
                   o => o.MapFrom(s => s.Attendees
                                        .FirstOrDefault(x => x.IsHost)!.UserName));

    CreateMap<ActivityAttendee, ActivityAttendeeDTO>();
    CreateMap<ActivityAttendee, UserInfoDTO>();
  }
}
