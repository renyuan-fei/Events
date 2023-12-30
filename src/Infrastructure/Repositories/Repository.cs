using Application.Common.Interfaces;

using Domain.Common;

using Infrastructure.DatabaseContext;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public abstract class Repository <TEntity, TEntityId>
where TEntity : BaseEntity<TEntityId>
where TEntityId : class
{
  protected readonly EventsDbContext DbContext;

  protected Repository(EventsDbContext dbContext) { DbContext = dbContext; }

  async protected Task AddAsync(TEntity entity)
  {
    await DbContext.AddAsync(entity);
  }

  protected Task Delete(TEntity entity)
  {
    DbContext.Remove(entity);

    return Task.CompletedTask;
  }

  async protected Task<TEntity?> GetByIdAsync(TEntityId id, CancellationToken cancellationToken = default)
  {
    return await DbContext.Set<TEntity>().FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
  }

}
