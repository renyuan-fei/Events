using Application.common.interfaces;

using Domain.Common.Contracts;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Data.Interceptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{
  private readonly IDateTimeService    _dateTime;
  private readonly ICurrentUserService _user;

  public AuditableEntityInterceptor(
      ICurrentUserService user,
      IDateTimeService    dateTime)
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

    foreach (var entry in context.ChangeTracker.Entries<IBaseAuditableEntity>())
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
  public static bool HasChangedOwnedEntities(this EntityEntry entry)
  {
    return entry.References.Any(r =>
                                    r.TargetEntry != null
                                 && r.TargetEntry.Metadata.IsOwned()
                                 && (r.TargetEntry.State == EntityState.Added
                                  || r.TargetEntry.State == EntityState.Modified));
  }
}
