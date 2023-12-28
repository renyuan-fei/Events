using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class ActivityAttendeeConfiguration : IEntityTypeConfiguration<ActivityAttendee>
{
  public void Configure(EntityTypeBuilder<ActivityAttendee> builder)
  {

  }
}
