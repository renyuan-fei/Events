using Domain.Events.Photo;
using Domain.ValueObjects.Photo;

namespace Domain.Entities;

public class Photo : BaseAuditableEntity<PhotoId>
{
  private Photo(PhotoId photoId, PhotoDetails details, UserId userId)
  {
    Id = photoId;
    Details = details;
    UserId = userId;
  }

  private Photo() { }

  public PhotoDetails Details { get; private set; }
  public UserId       UserId  { get; private set; }

  public static Photo Add(string publicId, string url, bool isMain, UserId userId)
  {
    var details = PhotoDetails.Create(publicId, url, isMain);
    var photo = new Photo(PhotoId.New(),details, userId);

    return photo;
  }

  public void Update()
  {
    Details = PhotoDetails.Create(Details.PublicId, Details.Url, !Details.IsMain);
  }
}
