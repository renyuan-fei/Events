using Domain.Repositories;

using Infrastructure.DatabaseContext;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
  private readonly AppIdentityDbContext _context;
  public UserRepository(AppIdentityDbContext context) { _context = context; }
}
