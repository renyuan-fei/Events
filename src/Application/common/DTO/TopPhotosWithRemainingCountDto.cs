namespace Application.common.DTO;

public class TopPhotosWithRemainingCountDto
{
  public List<PhotoDto> TopPhotos      { get; set; }
  public int            RemainingCount { get; set; }
}