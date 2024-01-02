namespace Domain.Repositories;

public interface IPhotoRepository
{
  Task AddAsync(Photo photo, CancellationToken cancellationToken);

  Task<Photo?> GetByPublicIdAsync(string publicId, CancellationToken cancellationToken);

  Task<Photo?> GetMainPhotoByUserIdAsync(
      UserId            userId,
      CancellationToken cancellationToken);

  public Task<IEnumerable<Photo>> GetMainPhotosByUserIdAsync(
      IEnumerable<UserId> userIds,
      CancellationToken   cancellationToken);

  void Remove(Photo photo);
}
