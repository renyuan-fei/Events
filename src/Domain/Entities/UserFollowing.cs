using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class UserFollowing : AuditableEntity
{
  [ Key ]
  public Guid Id { get; set; }

  public Guid FollowerId { get; set; }
  public Guid FolloweeId { get; set; }
}
