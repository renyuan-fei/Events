using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects.Message;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data.Configuration;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
  public void Configure(EntityTypeBuilder<Notification> builder)
  {
    builder.ToTable("Notifications");

    builder.HasKey(n => n.Id);

    builder.Property(n => n.Content).IsRequired().HasMaxLength(500);

    builder.Property(n => n.Type)
           .HasConversion(type => type.ToString(),
                          typeString => Enum.Parse<NotificationType>(typeString));

    builder.Property(n => n.Id)
           .HasConversion(notificationId => notificationId.Value,
                          value => new NotificationId(value));

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