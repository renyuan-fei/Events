using Application.common.DTO.Interface;

namespace Application.common.DTO;

public class CommentDTO : IUserDetail
{
  public string  Id          { get; set; }
  public string  UserId      { get; set; }
  public string? DisplayName { get; set; }
  public string? UserName    { get; set; }
  public string? Bio         { get; set; }
  public DateTime CreatedAt   { get; set; }
  public string   Body        { get; set; }
  public string   Image       { get; set; }
}
