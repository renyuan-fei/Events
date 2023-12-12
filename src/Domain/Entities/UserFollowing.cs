using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class UserFollowing : AuditableEntity
{
  [Key]
  public Guid Id { get; set; }
  public Guid ObserverId { get; set; }
  public Guid TargetId   { get; set; }
}
