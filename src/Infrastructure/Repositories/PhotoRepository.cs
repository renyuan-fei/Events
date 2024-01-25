using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using Domain.ValueObjects.Photo;

using Infrastructure.DatabaseContext;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PhotoRepository : Repository<Photo, PhotoId>, IPhotoRepository
{
  public PhotoRepository(EventsDbContext dbContext) : base(dbContext) { }

  public Task<Photo?> GetByPublicIdAsync(
      string            publicId,
      CancellationToken cancellationToken)
  {
    return DbContext.Photos.FirstOrDefaultAsync(p => p.Details.PublicId == publicId,
                                                cancellationToken);
  }

  public Task<List<Photo>> GetPhotosByOwnerIdAsync(
      string            ownerId,
      CancellationToken cancellationToken)
  {
    return DbContext.Photos.Where(p => p.OwnerId == ownerId).ToListAsync(
        cancellationToken);
  }

  public IQueryable<Photo> GetPhotosWithoutMainPhotoByOwnerIdQueryable(
      string            ownerId,
      CancellationToken cancellationToken)
  {
    return DbContext.Photos.Where(p => p.OwnerId == ownerId && !p.Details.IsMain).AsQueryable();
  }

  public Task<Photo?> GetMainPhotoByOwnerIdAsync(
      string            ownerId,
      CancellationToken cancellationToken)
  {
    return DbContext.Photos.FirstOrDefaultAsync(p => p.OwnerId == ownerId
                                                  && p.Details.IsMain == true,
                                                cancellationToken);
  }

  public async Task<IEnumerable<Photo>> GetMainPhotosByOwnerIdAsync(
      IEnumerable<string> ownerIds,
      CancellationToken   cancellationToken)
  {
    return await DbContext.Photos.Where(p => ownerIds.Contains(p.OwnerId)
                                          && p.Details.IsMain
                                          == true)
                          .ToListAsync(cancellationToken);
  }
}
