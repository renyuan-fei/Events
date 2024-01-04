using Application.common.Interfaces;

using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Infrastructure.DatabaseContext;

public class TransactionManager : ITransactionManager
{
  private readonly EventsDbContext             _dbContext;
  private readonly ILogger<TransactionManager> _logger;
  private          IDbContextTransaction?      _currentTransaction;

  public TransactionManager(EventsDbContext dbContext, ILogger<TransactionManager> logger)
  {
    _dbContext = dbContext;
    _logger = logger;
  }

  public async Task BeginTransactionAsync()
  {
    if (_currentTransaction != null) { return; }

    _logger.LogInformation("Starting a new transaction");
    _currentTransaction = await _dbContext.Database.BeginTransactionAsync();
  }

  public async Task CommitTransactionAsync()
  {
    if (_currentTransaction == null)
    {
      throw new InvalidOperationException("No active transaction to commit");
    }

    try
    {
      await _dbContext.SaveChangesAsync();
      await _currentTransaction.CommitAsync();
      _logger.LogInformation("Transaction committed successfully");
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error committing transaction");

      throw;
    }
    finally
    {
      _currentTransaction.Dispose();
      _currentTransaction = null;
    }
  }

  public async Task RollbackTransactionAsync()
  {
    if (_currentTransaction == null)
    {
      throw new InvalidOperationException("No active transaction to rollback");
    }

    try
    {
      await _currentTransaction.RollbackAsync();
      _logger.LogInformation("Transaction rolled back successfully");
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error rolling back transaction");

      throw;
    }
    finally
    {
      _currentTransaction.Dispose();
      _currentTransaction = null;
    }
  }
}
