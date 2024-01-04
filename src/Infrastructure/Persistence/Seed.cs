using Infrastructure.DatabaseContext;
using Infrastructure.Identity;

using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistence;

public static class Seed
{
  public async static Task SeedData(
      AppIdentityDbContext         dbContext,
      EventsDbContext              eventsDbContext,
      UserManager<ApplicationUser> userManager)
  {
  }
}
