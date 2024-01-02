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

  public static PhotoDetails Create(string publicId, string url, bool isMain)
  {
    return new PhotoDetails(publicId, url, isMain);
  }

  public string PublicId { get; private set; }
  public string Url      { get; private set; }
  public bool   IsMain   { get; private set; }

  protected override IEnumerable<object> GetEqualityComponents()
  {
    yield return PublicId;
    yield return Url;
    yield return IsMain;
  }
}
