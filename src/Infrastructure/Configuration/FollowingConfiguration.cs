using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class FollowingConfiguration : IEntityTypeConfiguration<Following>
{
  public void Configure(EntityTypeBuilder<Following> builder)
  {

  }
}
