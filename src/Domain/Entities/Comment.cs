using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Comment : AuditableEntity
{
  [ Key ]
  public Guid Id { get; set; }

  public string   Body     { get; set; }
  public Guid     AuthorId { get; set; }
  public Activity Activity { get; set; }
}