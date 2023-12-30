using Domain.Entities;
using Domain.ValueObjects.Activity;
using Domain.ValueObjects.ActivityAttendee;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class AttendeeConfiguration : IEntityTypeConfiguration<Attendee>
{
  public void Configure(EntityTypeBuilder<Attendee> builder)
  {
    builder.ToTable("Attendees");

    builder.HasKey(attendee => attendee.Id);

    builder.Property(attendee => attendee.Id)
           .HasConversion(attendeeId => attendeeId.Value,
                          value => new ActivityAttendeeId(value));

    builder.Property(attendee => attendee.ActivityId)
           .HasConversion(id => id.Value,                // 将 ActivityId 转换为基础数据类型
                          value => new ActivityId(value) // 将基础数据类型转换为 ActivityId
                         );

    builder.OwnsOne(attendee => attendee.Identity);

    builder.HasOne(attendee => attendee.Activity)
           .WithMany(attendees => attendees.Attendees)
           .HasForeignKey(attendee => attendee.ActivityId)
           .OnDelete(DeleteBehavior.Cascade);
  }
}
