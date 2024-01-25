namespace Domain.Repositories;

public interface IPhotoRepository
{
  Task AddAsync(Photo photo, CancellationToken cancellationToken);

  Task<Photo?> GetByPublicIdAsync(string publicId, CancellationToken cancellationToken);

  Task<List<Photo>> GetPhotosByOwnerIdAsync(string ownerId, CancellationToken cancellationToken);

  IQueryable<Photo> GetPhotosWithoutMainPhotoByOwnerIdQueryable(string ownerId, CancellationToken cancellationToken);

  Task<Photo?> GetMainPhotoByOwnerIdAsync(
      string            ownerId,
      CancellationToken cancellationToken);

  public Task<IEnumerable<Photo>> GetMainPhotosByOwnerIdAsync(
      IEnumerable<string> ownerIds,
      CancellationToken   cancellationToken);

  void Remove(Photo photo);
}
