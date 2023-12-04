using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class ActivityAttendee : AuditableEntity
{
  [ Key ]
  public Guid Id { get; set; } = Guid.NewGuid();

  public bool IsHost { get; set; }
}
