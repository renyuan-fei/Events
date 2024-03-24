using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects.Activity;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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

    var dateTimeConverter = new ValueConverter<DateTime, DateTimeOffset>(
       date => new DateTimeOffset(DateTime.SpecifyKind(date, DateTimeKind.Utc)),
       dateOffset => dateOffset.DateTime
      );

    // Apply the converter to the Date property
    builder.Property(activity => activity.Date)
           .HasConversion(dateTimeConverter);

    var dateTimeOffsetConverter = new ValueConverter<DateTimeOffset, DateTimeOffset>(
       v => v.ToUniversalTime(),
       v => v
      );

    builder.Property(e => e.Created)
           .HasConversion(dateTimeOffsetConverter);

    builder.Property(e => e.LastModified)
           .HasConversion(dateTimeOffsetConverter);

  }
}