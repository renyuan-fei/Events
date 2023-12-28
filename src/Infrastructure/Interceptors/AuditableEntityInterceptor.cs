using Application.common.interfaces;

using Domain.Common;

using Infrastructure.Service;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Interceptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{
  private readonly ICurrentUserService _user;
  private readonly DateTimeService     _dateTime;

  public AuditableEntityInterceptor(
      ICurrentUserService user,
      DateTimeService     dateTime)
  {
    _user = user;
    _dateTime = dateTime;
  }

  public override InterceptionResult<int> SavingChanges(
      DbContextEventData      eventData,
      InterceptionResult<int> result)
  {
    UpdateEntities(eventData.Context);

    return base.SavingChanges(eventData, result);
  }

  public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
      DbContextEventData      eventData,
      InterceptionResult<int> result,
      CancellationToken       cancellationToken = default)
  {
    UpdateEntities(eventData.Context);

    return base.SavingChangesAsync(eventData, result, cancellationToken);
  }

  private void UpdateEntities(DbContext? context)
  {
    if (context == null) return;

    foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity>())
    {
      if (entry.State == EntityState.Added)
      {
        entry.Entity.CreatedBy = _user.Id;
        entry.Entity.Created = _dateTime.Now;
      }

      if (entry.State != EntityState.Added
       && entry.State != EntityState.Modified
       && !entry.HasChangedOwnedEntities())
        continue;

      entry.Entity.LastModifiedBy = _user.Id;
      entry.Entity.LastModified = _dateTime.Now;
    }
  }
}

public static class Extensions
{
  public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
      entry.References.Any(r =>
                               r.TargetEntry != null
                            && r.TargetEntry.Metadata.IsOwned()
                            && (r.TargetEntry.State == EntityState.Added
                             || r.TargetEntry.State == EntityState.Modified));
}
