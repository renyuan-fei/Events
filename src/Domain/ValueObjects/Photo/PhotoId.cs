namespace Domain.ValueObjects.Photo;

public record PhotoId(string Value)
{
  public static PhotoId New() => new PhotoId(Guid.NewGuid().ToString());
}
