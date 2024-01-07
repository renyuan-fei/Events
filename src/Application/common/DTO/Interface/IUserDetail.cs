namespace Application.common.DTO.Interface;

public interface IUserDetail
{
  string  UserId      { get; }
  string? DisplayName { get; set; }
  string? UserName    { get; set; }
  string? Bio         { get; set; }
  string? Image       { get; set; }
}