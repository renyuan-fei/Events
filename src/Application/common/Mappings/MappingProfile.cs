using Application.common.DTO;

using AutoMapper;

using Domain.Entities;

namespace Application.common.Mappings;

public class MappingProfile : Profile
{
  public MappingProfile()
  {
    CreateMap<Activity, Activity>();
    CreateMap<Activity, ActivityDto>();
  }
}