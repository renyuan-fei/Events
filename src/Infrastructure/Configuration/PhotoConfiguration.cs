using Domain.Entities;
using Domain.ValueObjects;
using Domain.ValueObjects.Photo;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class PhotoConfiguration :  IEntityTypeConfiguration<Photo>
{
  public void Configure(EntityTypeBuilder<Photo> builder)
  {
    builder.ToTable("Photos");

    builder.HasKey(photo => photo.Id);

    builder.OwnsOne(photo => photo.Details);

    builder.Property(photo => photo.Id)
          .HasConversion(photoId => photoId.Value, value => new PhotoId(value));

    builder.Property(photo => photo.UserId)
           .HasConversion(userId => userId.Value, value => new UserId(value));
  }
}
