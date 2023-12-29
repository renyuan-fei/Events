namespace Domain.Common.Contracts;

public interface IBaseAuditableEntity
{
  DateTime Created { get; set; }

  string? CreatedBy { get; set; }

  DateTime? LastModified { get; set; }

  string? LastModifiedBy { get; set; }
}
