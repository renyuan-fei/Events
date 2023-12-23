using Domain;
using Domain.Entities;

using Infrastructure.DatabaseContext;
using Infrastructure.Identity;

using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistence;

public static class Seed
{
  public async static Task SeedData(
      AppIdentityDbContext         dbContext,
      EventsDbContext             eventsDbContext,
      UserManager<ApplicationUser> userManager)
  {
    var id2 = "80ff56fb-70be-42e3-866f-59900ba54dbc";
    var id3 = "bfd0cdcf-2c53-49bc-bd01-dd403a5b2f46";

    if (!userManager.Users.Any())
    {
      var users = new List<ApplicationUser>
      {
          new() { DisplayName = "Jane", UserName = "jane", Email = "jane@test.com" },
          new() { DisplayName = "Tom", UserName = "tom", Email = "tom@test.com" }
      };

      foreach (var user in users) { await userManager.CreateAsync(user, "Pa$$w0rd"); }

      await dbContext.SaveChangesAsync();
    }

    const string defaultImage = "https://res.cloudinary.com/dxwtrnpqi/image/upload/v1702453913/gyzjw6xpz9pzl0xg7de4.jpg";
    const string defaultImagePublicId = "gyzjw6xpz9pzl0xg7de4";
  }
}
