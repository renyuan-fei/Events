namespace Domain.Common;

public abstract class AuditableEntity
{
  public DateTimeOffset Created { get; set; }

  public Guid? CreatedBy { get; set; }

  public DateTimeOffset LastModified { get; set; }

  public Guid? LastModifiedBy { get; set; }
}
