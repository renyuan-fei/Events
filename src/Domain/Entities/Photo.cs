using Domain.ValueObjects.Photo;

namespace Domain.Entities;

public class Photo : BaseAuditableEntity<PhotoId>
{
  private Photo(PhotoId photoId, PhotoDetails details, string ownerId)
  {
    Id = photoId;
    Details = details;
    OwnerId = ownerId;
  }

  private Photo() { }

  public PhotoDetails Details { get; private set; }
  public string       OwnerId  { get; private set; }

  public static Photo Add(string publicId, string url, bool isMain, string ownerId)
  {
    var details = PhotoDetails.Create(publicId, url, isMain);
    var photo = new Photo(PhotoId.New(), details, ownerId);

    return photo;
  }

  public void Update()
  {
    Details = PhotoDetails.Create(Details.PublicId, Details.Url, !Details.IsMain);
  }
}
