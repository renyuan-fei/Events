using Application.common.DTO;

using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects.Activity;

namespace Application.common.Mappings;

public class MappingProfile : Profile
{
  public MappingProfile()
  {
    // ActivityDTO to Activity
    CreateMap<ActivityDto, Activity>()
        .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
        .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
        .ForMember(dest => dest.Category,
                   opt => opt.MapFrom(src => Enum.Parse<Category>(src.Category)))
        .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
        .ForMember(dest => dest.Location,
                   opt => opt.MapFrom(src => Address.From(src.City, src.Venue)))
        .ForMember(dest => dest.Status,
                   opt => opt.Ignore()) // Assuming status is not set directly from DTO
        .ForAllOtherMembers(opts => opts
                                .Ignore()); // Ignore other members not explicitly mapped

    CreateMap<Activity, ActivityWithAttendeeDto>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
        .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
        .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
        .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
        .ForMember(dest => dest.Category,
                   opt => opt.MapFrom(src => src.Category.ToString()))
        .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Location.City))
        .ForMember(dest => dest.Venue, opt => opt.MapFrom(src => src.Location.Venue))
        .ForMember(dest => dest.IsCancelled,
                   opt => opt.MapFrom(src => src.Status == ActivityStatus.Canceled))
        .ForMember(dest => dest.Attendees, opt => opt.MapFrom(src => src.Attendees))
        .ForMember(dest => dest.HostUser,
                   opt => opt.MapFrom(src =>
                                          new HostUserDTO
                                          {
                                              Id = src.Attendees.FirstOrDefault(a => a.Identity.IsHost)!.Identity.UserId.Value,
                                              Username = "",
                                              ImageUrl = "",
                                          }))
        .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());


    CreateMap<Attendee, AttendeeDto>()
        .ForMember(dest => dest.UserId,
                   opt => opt.MapFrom(src => src.Identity.UserId.Value))
        .ForMember(dest => dest.IsHost, opt => opt.MapFrom(src => src.Identity.IsHost))
        .ForMember(dest => dest.DisplayName, opt => opt.Ignore()) // fill in later
        .ForMember(dest => dest.UserName, opt => opt.Ignore())    // fill in later
        .ForMember(dest => dest.Bio, opt => opt.Ignore())         // fill in later
        .ForMember(dest => dest.Image, opt => opt.Ignore());      // fill in later

    CreateMap<Following, FollowingDTO>()
        .ForMember(dest => dest.UserId,
                   opt => opt.MapFrom(src => src.Relationship
                                                .FollowingId.Value))
        .ForMember(dest => dest.DisplayName,
                   opt => opt
                       .Ignore()) // Assuming you don't have this in Following entity
        .ForMember(dest => dest.UserName,
                   opt => opt
                       .Ignore()) // Assuming you don't have this in Following entity
        .ForMember(dest => dest.Bio,
                   opt => opt
                       .Ignore()) // Assuming you don't have this in Following entity
        .ForMember(dest => dest.Image,
                   opt => opt
                       .Ignore()); // Assuming you don't have this in Following entity

    CreateMap<Following, FollowerDto>()
        .ForMember(dest => dest.UserId,
                   opt => opt.MapFrom(src => src.Relationship.FollowerId.Value))
        .ForMember(dest => dest.DisplayName,
                   opt => opt
                       .Ignore()) // Assuming you don't have this in Following entity
        .ForMember(dest => dest.UserName,
                   opt => opt
                       .Ignore()) // Assuming you don't have this in Following entity
        .ForMember(dest => dest.Bio,
                   opt => opt
                       .Ignore()) // Assuming you don't have this in Following entity
        .ForMember(dest => dest.Image,
                   opt => opt
                       .Ignore()); // Assuming you don't have this in Following entity

    CreateMap<Activity, ActivityWithHostUserDto>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
        .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
        .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
        .ForMember(dest => dest.Category,
                   opt => opt.MapFrom(src => src.Category.ToString()))
        .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Location.City))
        .ForMember(dest => dest.Venue, opt => opt.MapFrom(src => src.Location.Venue))
        .ForMember(dest => dest.goingCount,
                   opt => opt.MapFrom(src => src.Attendees.Count))
        .ForMember(dest => dest.HostUser,
                   opt => opt.MapFrom(src =>
                                          new HostUserDTO
                                          {
                                              Id =
                                                  src.Attendees
                                                     .FirstOrDefault(a => a.Identity
                                                         .IsHost)!.Identity
                                                     .UserId.Value,
                                              Username = "",
                                              ImageUrl = "",
                                          }))
        .ForMember(dest => dest.ImageUrl, opt => opt.Ignore());

    CreateMap<Comment, CommentDto>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value)) // 假设 Id 是一个包装类型
        .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId.Value)) // 同上
        .ForMember(dest => dest.Body, opt => opt.MapFrom(src => src.Body))
        .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.Created.UtcDateTime))
        .ForMember(dest => dest.DisplayName, opt => opt.Ignore())
        .ForMember(dest => dest.UserName, opt => opt.Ignore())
        .ForMember(dest => dest.Bio, opt => opt.Ignore())
        .ForMember(dest => dest.Image, opt => opt.Ignore());

    CreateMap<UserDto, UserProfileDto>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
        .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
        .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.Bio))
        .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
        .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
        .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
        .ForMember(dest => dest.Image,
                   opt => opt.Ignore()) // Assuming this needs to be handled separately
        .ForMember(dest => dest.Followers,
                   opt => opt.Ignore()) // Assuming this needs to be handled separately
        .ForMember(dest => dest.Following,
                   opt => opt.Ignore()); // Assuming this needs to be handled separately

    CreateMap<Photo, PhotoDto>()
        .ForMember(dest => dest.PublicId, opt => opt.MapFrom(src => src.Details.PublicId))
        .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Details.Url));
  }
}
