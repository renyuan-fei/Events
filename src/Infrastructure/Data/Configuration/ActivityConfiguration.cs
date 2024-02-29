using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects.Activity;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
{
  public void Configure(EntityTypeBuilder<Activity> builder)
  {
    builder.ToTable("Activities");

    builder.HasKey(activity => activity.Id);

    builder.Property(activity => activity.Id)
           .HasConversion(activityId => activityId.Value, value => new ActivityId(value));

    builder.Property(activity => activity.Category)
           .HasConversion(category => category.ToString(),
                          categoryString => Enum.Parse<Category>(categoryString));

    builder.Property(activity => activity.Status)
           .HasConversion(status => status.ToString(),
                          statusString => Enum.Parse<ActivityStatus>(statusString));

    builder.OwnsOne(activity => activity.Location,
                    navigationBuilder =>
                    {
                      navigationBuilder.Property(location => location.City)
                                       .HasColumnName("City");

                      navigationBuilder.Property(location => location.Venue)
                                       .HasColumnName("Venue");
                    });
  }
}
