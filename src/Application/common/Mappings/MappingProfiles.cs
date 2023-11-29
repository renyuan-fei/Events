using System.Diagnostics;

using AutoMapper;

namespace Application.common.Mappings;

public class MappingProfiles : Profile
{
  public MappingProfiles()
  {
    CreateMap<Activity, Activity>();
  }
}
