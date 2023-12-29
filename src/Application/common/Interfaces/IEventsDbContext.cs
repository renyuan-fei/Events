using Domain;
using Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

/// <summary>
/// Represents the interface for the application database context.
/// </summary>
public interface IEventsDbContext
{
  /// <summary>
  /// Gets or sets the DbSet of activities.
  /// </summary>
  /// <value>
  /// The DbSet of activities.
  /// </value>
  DbSet<Activity> Activities { get; }

  /// <summary>
  /// Gets the DbSet of ActivityAttendees entities.
  /// </summary>
  /// <remarks>
  /// This property represents a collection of all the attendees for activities.
  /// </remarks>
  /// <value>
  /// The DbSet of ActivityAttendees entities.
  /// </value>
  DbSet<ActivityAttendee> ActivityAttendees { get; }

  /// <summary>
  /// Asynchronously saves all changes made in this context to the underlying database.
  /// </summary>
  /// <returns>
  /// A task that represents the asynchronous save operation. The task result contains the
  /// number of state entries written to the database.
  /// </returns>
  DbSet<Photo> Photos { get; }

  /// <summary>
  /// Gets or sets the collection of UserFollowings.
  /// </summary>
  /// <remarks>
  /// This property is a DbSet that represents the collection of UserFollowings in the database.
  /// DbSet provides functionality for querying, adding, modifying and deleting entities.
  /// </remarks>
  DbSet<Following> Followings { get; }

  /// <summary>
  /// Gets the DbSet of comments.
  /// </summary>
  /// <remarks>
  /// This property represents the collection of comments in the database.
  /// </remarks>
  /// <value>The DbSet of comments.</value>
  DbSet<Comment> Comments { get; }

  /// <summary>
  /// Asynchronously saves all changes made in this context to the underlying database.
  /// <paramref name="cancellationToken"/> can be used to request that the operation be
  /// cancelled.
  /// </summary>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <returns>A task that represents the asynchronous save operation. The task result
  /// contains the number of state entries written to the underlying database.
  /// </returns>
  /// <exception cref="DbUpdateException">An error occurred while saving to the database.</exception>
  /// <exception cref="DbUpdateConcurrencyException">A concurrency violation is encountered
  /// while saving to the database. This is usually caused by a database trigger that
  /// indicates another user has changed a record in the database.</exception>
  /// <exception cref="InvalidOperationException">Some error occurred attempting to process
  /// entities in the context either before or after sending commands to the database.</exception>
  /// <exception cref="NotSupportedException">An attempt was made to use unsupported behavior
  /// such as executing multiple asynchronous commands concurrently on the same context
  /// instance.</exception>
  /// <exception cref="ObjectDisposedException">The context or connection have been disposed.</exception>
  /// <exception cref="OperationCanceledException">If the <paramref name="cancellationToken"/> is
  /// cancelled.</exception>
  Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
