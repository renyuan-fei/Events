using System.ComponentModel.DataAnnotations;

using Application.common.DTO.Validator;

using Domain.Enums;

namespace Application.common.DTO;

/// <summary>
/// Represents an activity data transfer object.
/// </summary>
public class ActivityDTO
{
  /// <summary>
  /// Gets or sets the Title.
  /// </summary>
  /// <remarks>
  /// The Title is a required property and must be at most 20 words long.
  /// </remarks>
  /// <value>
  /// The Title.
  /// </value>
  [ Required ]
  [ MaxWordCount(10, ErrorMessage = "Title must be at most 10 words long.") ]
  public string Title { get; set; }

  /// <summary>
  /// Gets or sets the date value. This property is required and should be greater than the current date.
  /// </summary>
  /// <remarks>
  /// The <see cref="Date"/> property must be at least one day after the current date.
  /// </remarks>
  [ Required ]
  [ DateGreaterThanToday(ErrorMessage =
                              "Date must be at least one day after the current date.") ]
  public DateTime Date { get; set; }

  /// <summary>
  /// Represents the category of an item.
  /// </summary>
  [ Required ]
  [ EnumDataType(typeof(Category), ErrorMessage = "Category must be a valid") ]
  public string Category { get; set; }

  /// <summary>
  /// Gets or sets the description of the property.
  /// </summary>
  /// <remarks>
  /// The description is required and must be at most 100 words long.
  /// </remarks>
  /// <value>
  /// The description as a string.
  /// </value>
  [ Required ]
  [ MaxWordCount(100, ErrorMessage = "Description must be at most 100 words long.") ]
  public string Description { get; set; }

  /// <summary>
  /// Gets or sets the city of a property.
  /// </summary>
  /// <remarks>
  /// This property is required.
  /// </remarks>
  /// <value>
  /// A string representing the city of a property.
  /// </value>
  [ Required ]
  public string City { get; set; }

  /// <summary>
  /// Represents the venue of an event.
  /// </summary>
  /// <remarks>
  /// This property must be provided and cannot be null or empty.
  /// </remarks>
  /// <value>
  /// The name or location of the venue.
  /// </value>
  [ Required ]
  public string Venue { get; set; }
}
