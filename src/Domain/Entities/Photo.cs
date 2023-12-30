using Domain.ValueObjects.Photo;

namespace Domain.Entities;

public class Photo : BaseAuditableEntity<PhotoId>
{
  private Photo(PhotoDetails details, UserId userId)
  {
    Details = details;
    UserId = userId;
  }

  private Photo() {
  }

  public PhotoDetails Details { get; private set; }
  public UserId       UserId  { get; private set; }

  public static Photo Add(string publicId, string url, bool isMain, UserId userId)
  {
    var details = new PhotoDetails(publicId, url, isMain);

    return new Photo(details, userId);
  }

  public static Photo Remove(string publicId, UserId userId)
  {
    throw new NotImplementedException();
  }
}
