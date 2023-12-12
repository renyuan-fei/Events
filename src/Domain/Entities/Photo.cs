namespace Domain;

public class Photo : AuditableEntity
{
  public Guid   Id     { get; set; } = Guid.NewGuid();
  public string Url    { get; set; }
  public bool   IsMain { get; set; }
}
