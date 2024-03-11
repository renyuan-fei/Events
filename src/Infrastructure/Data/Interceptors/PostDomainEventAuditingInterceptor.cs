using Application.common.interfaces;

using Domain.Common;
using Domain.Common.Contracts;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Data.Interceptors;

public class PostDomainEventAuditingInterceptor : SaveChangesInterceptor
{
  private readonly IDateTimeService    _dateTime;
  private readonly ICurrentUserService _user;

  public PostDomainEventAuditingInterceptor(IDateTimeService dateTime, ICurrentUserService user)
  {
    _dateTime = dateTime;
    _user = user;
  }

  public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
  {
    UpdateEntities(eventData.Context);
    return base.SavingChanges(eventData, result);
  }

  public async override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
  {
    UpdateEntities(eventData.Context);

    return await base.SavingChangesAsync(eventData, result, cancellationToken);
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

      if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
      {
        entry.Entity.LastModifiedBy = _user.Id;
        entry.Entity.LastModified = _dateTime.Now;
      }
    }
  }
}
