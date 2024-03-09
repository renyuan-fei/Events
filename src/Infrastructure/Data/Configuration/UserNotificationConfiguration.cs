using Domain.Entities;
using Domain.ValueObjects;
using Domain.ValueObjects.Message;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class UserNotificationConfiguration : IEntityTypeConfiguration<UserNotification>
{
  public void Configure(EntityTypeBuilder<UserNotification> builder)
  {
    builder.ToTable("UserNotifications");

    // 设置复合主键
    builder.HasKey(un => un.Id);

    // 配置关系
    builder.HasOne(un => un.Notification)
           .WithMany()
           .HasForeignKey(un => un.NotificationId)
           .IsRequired();

    builder.Property(un => un.UserId)
           .HasConversion(userId => userId.Value, value => new UserId(value));

    builder.Property(un => un.IsRead).IsRequired();

    builder.Property(un => un.Id)
           .HasConversion(userNotificationId => userNotificationId.Value,
                          value => new UserNotificationId(value));
  }
}
