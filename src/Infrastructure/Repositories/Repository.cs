using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Domain.Common;

using Infrastructure.DatabaseContext;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public abstract class Repository<TEntity, TEntityId>
where TEntity : BaseEntity<TEntityId>
where TEntityId : class
{
  protected readonly EventsDbContext DbContext;

  protected Repository(EventsDbContext dbContext)
  {
    DbContext = dbContext;
  }

  public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
  {
    await DbContext.AddAsync(entity, cancellationToken);
  }

  public void Remove(TEntity entity)
  {
    DbContext.Remove(entity);
  }

  public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
  {
    return await DbContext.Set<TEntity>().ToListAsync(cancellationToken);
  }

  public async Task<TEntity?> GetByIdAsync(TEntityId id, CancellationToken cancellationToken = default)
  {
    return await DbContext.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken);
  }
}