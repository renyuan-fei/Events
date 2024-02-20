namespace Application.common.DTO;

public class TopPhotosWithRemainingCountDto
{
  public List<PhotoDto> Photos      { get; set; }
  public int            RemainingCount { get; set; }
}