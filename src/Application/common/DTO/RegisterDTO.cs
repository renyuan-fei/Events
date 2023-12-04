using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

namespace Application.common.DTO;

public class RegisterDTO
{
  [ Required(ErrorMessage = "Display name is required") ]
  public string DisplayName { get; set; } = string.Empty;

  [ Required(ErrorMessage = "Email is required") ]
  [ EmailAddress(ErrorMessage = "Email is not valid") ]
  [ Remote("IsEmailAlreadyRegistered",
           "Account",
           ErrorMessage = "Email is already is use") ]
  public string Email { get; set; } = string.Empty;

  [ Required(ErrorMessage = "Phone number is required") ]
  [ RegularExpression("^[0-9]*$",
                      ErrorMessage = "Phone number should contain digits only") ]
  [ Remote("IsEmailAlreadyRegister",
           "Account",
           ErrorMessage = "Email is already is use") ]
  public string? PhoneNumber { get; set; } = string.Empty;

  [ Required(ErrorMessage = "Password is required") ]
  public string Password { get; set; } = string.Empty;

  [ Required(ErrorMessage = "Confirm password is required") ]
  [ Compare("Password", ErrorMessage = "Password and confirm password do not match") ]
  public string ConfirmPassword { get; set; } = string.Empty;
}
