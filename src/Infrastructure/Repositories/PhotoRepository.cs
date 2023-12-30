using Domain.Entities;
using Domain.ValueObjects.Photo;

using Infrastructure.DatabaseContext;

namespace Infrastructure.Repositories;

public class PhotoRepository : Repository<Photo,PhotoId>
{
  public PhotoRepository(EventsDbContext dbContext) : base(dbContext)
  {
  }
}
