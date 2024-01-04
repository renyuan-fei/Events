using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

public class MaxWordCountAttribute : ValidationAttribute
{
  private readonly int _maxWordCount;

  public MaxWordCountAttribute(int maxWordCount) { _maxWordCount = maxWordCount; }

  protected override ValidationResult IsValid(
      object            value,
      ValidationContext validationContext)
  {
    if (value == null
     || string.IsNullOrWhiteSpace(value.ToString()))
    {
      return ValidationResult.Success; // Assume empty string is allowed.
    }

    var wordCount = Regex.Matches(value.ToString(), @"\b[\w']+\b").Count;

    if (wordCount > _maxWordCount)
    {
      return new
          ValidationResult($"The field {validationContext.DisplayName} must be at most {_maxWordCount} words long.");
    }

    return ValidationResult.Success;
  }
}
