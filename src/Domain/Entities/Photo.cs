using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Photo : AuditableEntity
{
  [ Key ]
  public string PublicId { get; set; }
  public Guid   UserId   { get; set; }
  public string Url      { get; set; }
  public bool   IsMain   { get; set; }
}
