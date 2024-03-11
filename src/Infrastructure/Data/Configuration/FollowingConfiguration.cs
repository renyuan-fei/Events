using Domain.Entities;
using Domain.ValueObjects;
using Domain.ValueObjects.Following;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class FollowingConfiguration : IEntityTypeConfiguration<Following>
{
  public void Configure(EntityTypeBuilder<Following> builder)
  {
    builder.ToTable("Following");

    builder.HasKey(following => following.Id);

    builder.Property(following => following.Id)
           .HasConversion(followingId => followingId.Value,
                          followingId => new FollowingId(followingId));

    // 配置 UserRelationship 作为 Owned Entity
    builder.OwnsOne(following => following.Relationship,
                    navigationBuilder =>
                    {
                      navigationBuilder.Property(relationship => relationship.FollowerId)
                                       .HasColumnName("FollowerId")
                                       .HasConversion(followerId => followerId.Value,
                                                      followerId =>
                                                          new UserId(followerId));

                      navigationBuilder.Property(relationship => relationship.FollowingId)
                                       .HasColumnName("FollowingId")
                                       .HasConversion(followeeId => followeeId.Value,
                                                      followeeId =>
                                                          new UserId(followeeId));
                    });
  }
}