using Infrastructure.DatabaseContext;
using Infrastructure.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Persistence;

using System.Reflection;
using System.IO;

public static class Seed
{

  public async static Task SeedData(
      ApplicationDbContext         context,
      UserManager<ApplicationUser> userManager)
  {
    var id1 = "18cab3f8-acbd-4a1b-b29b-3441e91e54b1";
    var id2 = "80ff56fb-70be-42e3-866f-59900ba54dbc";
    var id3 = "bfd0cdcf-2c53-49bc-bd01-dd403a5b2f46";

    if (!userManager.Users.Any()
     && !context.Activities.Any())
    {
      var users = new List<ApplicationUser>
      {
          new() { DisplayName = "Bob", UserName = "bob", Email = "bob@test.com" },
          new() { DisplayName = "Jane", UserName = "jane", Email = "jane@test.com" },
          new() { DisplayName = "Tom", UserName = "tom", Email = "tom@test.com" }
      };

      foreach (var user in users) { await userManager.CreateAsync(user, "Pa$$w0rd"); }

      await context.SaveChangesAsync();
    }
  }
}
