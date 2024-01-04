namespace Application.common.Interfaces;

/// <summary>
///   Represents an interface for managing transactions.
/// </summary>
public interface ITransactionManager
{
  /// <summary>
  ///   Begins a new database transaction asynchronously.
  /// </summary>
  /// <returns>A <see cref="Task" /> representing the asynchronous operation.</returns>
  Task BeginTransactionAsync();

  /// <summary>
  ///   Commits the current transaction asynchronously.
  /// </summary>
  /// <returns>A task that represents the asynchronous commit operation.</returns>
  Task CommitTransactionAsync();

  /// <summary>
  ///   Rolls back the current transaction asynchronously.
  /// </summary>
  /// <returns>A task representing the asynchronous rollback operation.</returns>
  Task RollbackTransactionAsync();
}
