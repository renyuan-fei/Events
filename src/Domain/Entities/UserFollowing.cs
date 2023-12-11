namespace Domain;

public class UserFollowing : AuditableEntity
{
  public Guid ObserverId { get; set; }
  public Guid TargetId   { get; set; }
}
