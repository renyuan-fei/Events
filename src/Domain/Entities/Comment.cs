using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Comment : AuditableEntity
{
  [ Key ]
  public Guid Id { get; set; }
  public string   Body     { get; set; }
  public Guid ActivityId { get; set; }
  public Activity Activity { get; set; }
}