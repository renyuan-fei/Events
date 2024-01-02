using Domain.Entities;
using Domain.ValueObjects;
using Domain.ValueObjects.Activity;
using Domain.ValueObjects.Comment;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
  public void Configure(EntityTypeBuilder<Comment> builder)
  {
    builder.ToTable("Comments");

    builder.HasKey(comment => comment.Id);

    builder.Property(comment => comment.Id)
           .HasConversion(commentId => commentId.Value,
                          commentId => new CommentId(commentId));

    builder.Property(comment => comment.UserId)
           .HasConversion(userId => userId.Value, userId => new UserId(userId));

    builder.Property(comment => comment.ActivityId)
           .HasConversion(activityId => activityId.Value,
                          activityId => new ActivityId(activityId));

    builder.HasOne(comment => comment.Activity)
           .WithMany(activity => activity.Comments)
           .HasForeignKey(comment => comment.ActivityId)
           .OnDelete(DeleteBehavior.Cascade);
  }
}
