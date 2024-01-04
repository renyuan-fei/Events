namespace Domain.ValueObjects.Photo;

public class PhotoDetails : ValueObject
{
  private PhotoDetails() { }

  private PhotoDetails(string publicId, string url, bool isMain)
  {
    PublicId = publicId;
    Url = url;
    IsMain = isMain;
  }

  public string PublicId { get; }
  public string Url      { get; }
  public bool   IsMain   { get; }

  public static PhotoDetails Create(string publicId, string url, bool isMain)
  {
    return new PhotoDetails(publicId, url, isMain);
  }

  protected override IEnumerable<object> GetEqualityComponents()
  {
    yield return PublicId;
    yield return Url;
    yield return IsMain;
  }
}
