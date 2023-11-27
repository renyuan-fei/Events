using System.ComponentModel.DataAnnotations;

using Domain.Identity;

namespace Domain.Entities;

public class ActivityAttendee
{
  [ Key ]
  public Guid Id { get; set; } = Guid.NewGuid();

  public bool IsHost { get; set; }

  public ApplicationUser AppUser { get; set; }
}
