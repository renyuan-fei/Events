namespace Domain.ValueObjects.Photo;

public record PhotoId(string Value)
{
  public static PhotoId New() { return new PhotoId(Guid.NewGuid().ToString()); }
}
