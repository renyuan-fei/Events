namespace Domain.ValueObjects.Photo;

public class PhotoDetails : ValueObject
{
  public string PublicId { get; private set; }
  public string Url      { get; private set; }
  public bool   IsMain   { get; private set; }

  protected override IEnumerable<object> GetEqualityComponents()
  {
    throw new NotImplementedException();
  }
}
