using Domain.Entities;
using Domain.ValueObjects;
using Domain.ValueObjects.Activity;
using Domain.ValueObjects.ActivityAttendee;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data.Configuration;

public class AttendeeConfiguration : IEntityTypeConfiguration<Attendee>
{
  public void Configure(EntityTypeBuilder<Attendee> builder)
  {
    builder.ToTable("Attendees");

    builder.HasKey(attendee => attendee.Id);

    builder.Property(attendee => attendee.Id)
           .HasConversion(attendeeId => attendeeId.Value,
                          value => new AttendeeId(value));

    builder.Property(attendee => attendee.ActivityId)
           .HasConversion(id => id.Value,
                          value => new ActivityId(value));

    builder.OwnsOne(attendee => attendee.Identity,
                    navigationBuilder =>
                    {
                      navigationBuilder.Property(identity => identity.UserId)
                                       .HasConversion(userId => userId.Value,
                                                      value => new UserId(value))
                                       .HasColumnName("UserId"); // 指定映射到的列名

                      navigationBuilder.Property(identity => identity.IsHost)
                                       .HasColumnName("IsHost"); // 指定映射到的列名
                    });

    builder.HasOne(attendee => attendee.Activity)
           .WithMany(attendees => attendees.Attendees)
           .HasForeignKey(attendee => attendee.ActivityId)
           .OnDelete(DeleteBehavior.Cascade);
  }
}