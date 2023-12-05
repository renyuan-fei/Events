using System.ComponentModel.DataAnnotations;

namespace Application.common.DTO;

public class LoginDTO
{
  [ Required(ErrorMessage = "Email is required") ]
  [ EmailAddress(ErrorMessage = "Email is not valid") ]
  public string Email { get; set; } = string.Empty;

  [ Required(ErrorMessage = "Password is required") ]
  public string Password { get; set; } = string.Empty;
}
