using Domain.Entities;
using Domain.ValueObjects;
using Domain.ValueObjects.Message;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data.Configuration;

public class UserNotificationConfiguration : IEntityTypeConfiguration<UserNotification>
{
  public void Configure(EntityTypeBuilder<UserNotification> builder)
  {
    builder.ToTable("UserNotifications");

    builder.HasKey(un => un.Id);

    builder.HasOne(un => un.Notification)
           .WithMany(n => n.UserNotifications) // 明确指明导航属性的名称
           .HasForeignKey(un => un.NotificationId)
           .IsRequired();

    builder.Property(un => un.UserId)
           .HasConversion(userId => userId.Value, value => new UserId(value));

    builder.Property(un => un.IsRead).IsRequired();

    builder.Property(un => un.Id)
           .HasConversion(userNotificationId => userNotificationId.Value,
                          value => new UserNotificationId(value));

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
