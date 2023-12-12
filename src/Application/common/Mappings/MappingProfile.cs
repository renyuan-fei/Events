using Application.common.DTO;
using Application.common.Models;

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
    // for update activity
    CreateMap<Activity, Activity>()
        .ForMember(activity => activity.Id, opt => opt.Ignore())
        .ForMember(activity => activity.Attendees, opt => opt.Ignore());

    // for transfer activity
    // but dont map attendees, it will be map later in controller(add user info)
    CreateMap<Activity, ActivityDTO>()
        .ForMember(activity => activity.Attendees, opt => opt.Ignore());

    // for transfer activity attendee
    CreateMap<ActivityAttendee, ActivityAttendeeDTO>();

    // for transfer user info to activity attendee
    // map userInfo's Id to ActivityAttendeeDTO's UserId
    CreateMap<UserInfoDTO, ActivityAttendeeDTO>()
        .ForMember(attendee => attendee.UserId, opt => opt.MapFrom(dto => dto.Id));

    // for transfer user info
    CreateMap<ActivityAttendee, UserInfoDTO>();

    CreateMap<PaginatedList<Activity>, PaginatedList<ActivityDTO>>()
        .ConvertUsing((src, dest, context) =>
        {
          var mappedItems = context.Mapper.Map<List<ActivityDTO>>(src.Items);
          return new PaginatedList<ActivityDTO>(mappedItems, src.TotalCount, src.PageNumber, src.TotalPages);
        });

  }
}
