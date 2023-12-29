namespace Domain.ValueObjects.Photo;

public record PhotoId(Guid Value)
{
  public static PhotoId New() => new PhotoId(Guid.NewGuid());
}
