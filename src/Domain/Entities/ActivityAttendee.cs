using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class ActivityAttendee : AuditableEntity
{
  [ Key ]
  public Guid Id { get; set; } = Guid.NewGuid();

  public bool IsHost { get; set; }

  /// <summary>
  ///   when create ActivityAttendee , query the user's Id ,and set it
  ///   Don't reference it as ApplicationUser type, it's bad
  /// </summary>
  public Guid UserId { get; set; }
}
