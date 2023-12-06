namespace Application.common.DTO;

public class AuthenticationDTO
{
  public string?  Token        { get; set; } = string.Empty;
  public DateTime Expiration   { get; set; }
  public string?  RefreshToken { get; set; } = string.Empty;

  public DateTime RefreshTokenExpirationDateTime { get; set; }
}
