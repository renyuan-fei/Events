using Application.common.interfaces;

using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class ApplicationUser : IdentityUser<Guid>, IApplicationUser
{
  public string? DisplayName  { get; set; }
  public string? Bio          { get; set; }
  public string? RefreshToken { get; set; }

  public DateTime RefreshTokenExpirationDateTime { get; set; }
  // public ICollection<ActivityAttendee> Activities  { get; set; }
  // public ICollection<Photo>            Photos      { get; set; }
  // public ICollection<UserFollowing>    Followings  { get; set; }
  // public ICollection<UserFollowing>    Followers   { get; set; }
}
