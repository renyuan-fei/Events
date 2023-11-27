using Microsoft.AspNetCore.Identity;

namespace Domain.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
  public string DisplayName { get; set; }
}
