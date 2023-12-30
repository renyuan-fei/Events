using Domain.Entities;
using Domain.ValueObjects.Activity;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
{
  public void Configure(EntityTypeBuilder<Activity> builder)
  {
    builder.ToTable("Activities");

    builder.HasKey(activity => activity.Id);

    builder.Property(activity => activity.Id)
           .HasConversion(activityId => activityId.Value, value => new ActivityId(value));

    builder.OwnsOne(activity => activity.Location);
  }
}
