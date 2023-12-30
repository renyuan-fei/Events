using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects.Photo;

using Infrastructure.DatabaseContext;

namespace Infrastructure.Repositories;

public class PhotoRepository : Repository<Photo,PhotoId>, IPhotoRepository
{
  public PhotoRepository(EventsDbContext dbContext) : base(dbContext)
  {
  }
}
