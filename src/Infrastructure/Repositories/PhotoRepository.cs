using System.Threading;
using System.Threading.Tasks;

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

  public Task<Photo?> GetMainPhotoByUserIdAsync(
      UserId            userId,
      CancellationToken cancellationToken)
  {
    return DbContext.Photos.FirstOrDefaultAsync(p => p.UserId == userId
                                                  && p.Details.IsMain == true,
                                                cancellationToken);
  }

  public async Task<IEnumerable<Photo>> GetMainPhotosByUserIdAsync(
      IEnumerable<UserId>
          userIds,
      CancellationToken cancellationToken)
  {
    return await DbContext.Photos.Where(p => userIds.Contains(p.UserId)
                                          && p.Details.IsMain
                                          == true)
                          .ToListAsync(cancellationToken);
  }
}
