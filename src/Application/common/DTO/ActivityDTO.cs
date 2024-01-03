using System.ComponentModel.DataAnnotations;

using Application.common.DTO.Validator;

using Domain.Enums;

namespace Application.common.DTO;

public class ActivityDTO
{
  [Required]
  [MaxWordCount(20, ErrorMessage = "Title must be at most 20 words long.")]
  public string Title { get; set; }

  [Required]
  [DateGreaterThanToday(ErrorMessage = "Date must be at least one day after the current date.")]
  public DateTime Date { get; set; }

  [Required]
  [EnumDataType(typeof(Category), ErrorMessage = "Category must be a valid")]
  public string Category { get; set; }

  [Required]
  [MaxWordCount(100, ErrorMessage = "Description must be at most 100 words long.")]
  public string Description { get; set; }

  [Required]
  public string City { get; set; }

  [Required]
  public string Venue { get; set; }
}
