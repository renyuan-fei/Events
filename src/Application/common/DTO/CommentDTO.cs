namespace Application.common.DTO;

public class CommentDTO
{
  public Guid     Id          { get; set; }
  public Guid     UserId      { get; set; }
  public DateTime CreatedAt   { get; set; }
  public string   Body        { get; set; }
  public string   Username    { get; set; }
  public string   DisplayName { get; set; }
  public string   Image       { get; set; }
}
