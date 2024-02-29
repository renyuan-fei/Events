using Domain.Entities;
using Domain.ValueObjects.Photo;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
{
  public void Configure(EntityTypeBuilder<Photo> builder)
  {
    builder.ToTable("Photos");

    builder.HasKey(photo => photo.Id);

    builder.OwnsOne(photo => photo.Details,
                    detailsBuilder =>
                    {
                      detailsBuilder.Property(d => d.PublicId).HasColumnName("PublicId");
                      detailsBuilder.Property(d => d.Url).HasColumnName("Url");
                      detailsBuilder.Property(d => d.IsMain).HasColumnName("IsMain");
                    });

    builder.Property(photo => photo.Id)
           .HasConversion(photoId => photoId.Value, value => new PhotoId(value));
  }
}
