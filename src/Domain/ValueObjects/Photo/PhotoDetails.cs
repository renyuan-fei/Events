namespace Domain.ValueObjects.Photo;

public class PhotoDetails : ValueObject
{
  public string PublicId { get; private set; }
  public string Url      { get; private set; }
  public bool   IsMain   { get; private set; }

  // ... 构造函数和方法 ...
  protected override IEnumerable<object> GetEqualityComponents() { throw new NotImplementedException(); }
}
