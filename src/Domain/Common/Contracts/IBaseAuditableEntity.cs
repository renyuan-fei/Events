namespace Domain.Common.Contracts;

public interface IBaseAuditableEntity
{
  DateTimeOffset Created { get; set; }

  string? CreatedBy { get; set; }

  DateTimeOffset LastModified { get; set; }

  string? LastModifiedBy { get; set; }
}
