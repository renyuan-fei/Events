namespace Application.common.DTO;

public class AccountResponseDTO
{
  public string? DisplayName { get; set; } = string.Empty;
  public string? Email       { get; set; } = string.Empty;
  public string? Token { get; set; } = string.Empty;

  public DateTime ExpirationDateTime { get; set; } = DateTime.MinValue;
}
