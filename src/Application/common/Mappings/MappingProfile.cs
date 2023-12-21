using Application.common.DTO;
using Application.common.Models;

using AutoMapper;

using Domain;
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
        .ForMember(attendee => attendee.UserId, opt => opt.MapFrom(dto => dto.Id))
        .ForMember(attendee => attendee.Image, opt => opt.MapFrom(dto => dto.MainPhoto));

    // for transfer user info
    CreateMap<ActivityAttendee, UserInfoDTO>();

    CreateMap<PaginatedList<Activity>, PaginatedList<ActivityDTO>>()
        .ConvertUsing((src, dest, context) =>
        {
          var mappedItems = context.Mapper.Map<List<ActivityDTO>>(src.Items);

          return new PaginatedList<ActivityDTO>(mappedItems,
                                                src.TotalCount,
                                                src.PageNumber,
                                                src.TotalPages);
        });

    // for transfer photo
    CreateMap<Photo, PhotoDTO>()
        .ForMember(photoDTO => photoDTO.Id, opt => opt.MapFrom(photo => photo.PublicId));

    CreateMap<Comment, CommentDTO>()
        .ForMember(commentDTO => commentDTO.CreatedAt,
                   opt => opt.MapFrom(comment => comment.Created.DateTime))
        .ForMember(commentDTO => commentDTO.UserId,
                   opt => opt.MapFrom(comment =>
                                          comment.CreatedBy))
        .ForMember(commonDTO => commonDTO.Body,
                   opt => opt.MapFrom(comment => comment.Body))
        .ForMember(commonDTO => commonDTO.Id,
                   opt => opt.MapFrom(comment => comment.Id))
        .ForMember(commentDTO => commentDTO.Username,
                   opt => opt.Ignore())
        .ForMember(commentDTO => commentDTO.Image, opt => opt.Ignore())
        .ForMember(commentDTO => commentDTO.DisplayName, opt => opt.Ignore());
  }
}
