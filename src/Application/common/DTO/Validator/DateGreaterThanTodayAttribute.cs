using System;
using System.ComponentModel.DataAnnotations;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace Application.common.DTO.Validator;

public class DateGreaterThanTodayAttribute : ValidationAttribute
{
  protected override ValidationResult IsValid(object value, ValidationContext validationContext)
  {
    switch (value)
    {
      case null :
        // if value is null, return error message
        return new ValidationResult(ErrorMessage ?? "Date is required.");

      case DateTime dateTime :
      {
        // if value is date and time not greater than today, return an error message
        if (dateTime < DateTime.Now.AddDays(1))
        {
          return new ValidationResult(ErrorMessage ?? "Date must be at least one day after the current date.");
        }

        break;
      }

      default :
        // if value is not a DateTime, return an error message
        return new ValidationResult(ErrorMessage ?? "Invalid date format.");
    }

    return ValidationResult.Success;
  }
}
