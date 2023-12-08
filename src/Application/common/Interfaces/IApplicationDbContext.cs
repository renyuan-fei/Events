using Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

/// <summary>
///   Represents the interface for the application database context.
/// </summary>
public interface IApplicationDbContext
{
  /// <summary>
  ///   Gets or sets the DbSet of activities.
  /// </summary>
  /// <value>
  ///   The DbSet of activities.
  /// </value>
  DbSet<Activity> Activities { get; }

  /// <summary>
  ///   Gets the <see cref="DbSet{T}" /> of <see cref="ActivityAttendee" /> entities.
  /// </summary>
  /// <remarks>
  ///   This property represents a collection of all the attendees for activities.
  /// </remarks>
  /// <value>
  ///   The <see cref="DbSet{T}" /> of <see cref="ActivityAttendee" /> entities.
  /// </value>
  DbSet<ActivityAttendee> ActivityAttendees { get; }

  /// <summary>
  ///   Asynchronously saves all changes made in this context to the underlying database.
  /// </summary>
  /// <param name="cancellationToken">
  ///   A <see cref="CancellationToken" /> to observe while waiting for the task to complete.
  /// </param>
  /// <returns>
  ///   A task that represents the asynchronous save operation. The task result contains the
  ///   number of state entries written to the database.
  /// </returns>
  Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
