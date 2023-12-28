using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class UserFollowingConfiguration : IEntityTypeConfiguration<UserFollowing>
{
  public void Configure(EntityTypeBuilder<UserFollowing> builder)
  {

  }
}
