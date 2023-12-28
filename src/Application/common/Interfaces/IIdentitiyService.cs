using Application.common.Models;

namespace Application.common.Interfaces;

/// <summary>
/// Represents a service for managing user identity.
/// </summary>
public interface IIdentityService
{
  /// <summary>
  /// Retrieves the username for a given user ID asynchronously.
  /// </summary>
  /// <param name="userId">The ID of the user.</param>
  /// <returns>
  /// A <see cref="Task{TResult}"/> representing the asynchronous operation.
  /// The task result contains the username as a string, or null if the user ID is invalid or the username is not available.
  /// </returns>
  Task<string?> GetUserNameAsync(string userId);

  /// <summary>
  /// Checks whether a user with the specified user ID is in the given role asynchronously.
  /// </summary>
  /// <param name="userId">The ID of the user to check.</param>
  /// <param name="role">The role to check.</param>
  /// <returns>A task that represents the asynchronous operation.
  /// The task result contains a boolean value indicating if the user is in the specified role.</returns>
  Task<bool> IsInRoleAsync(string userId, string role);

  /// <summary>
  /// Authorizes the user with the specified user ID against the given policy name asynchronously.
  /// </summary>
  /// <param name="userId">The identifier of the user to authorize.</param>
  /// <param name="policyName">The name of the policy to authorize against.</param>
  /// <returns>
  /// A task representing the asynchronous operation. The task result contains a boolean value indicating
  /// whether the user is authorized to access the resources protected by the specified policy.
  /// </returns>
  Task<bool> AuthorizeAsync(string userId, string policyName);

  /// <summary>
  /// Creates a new user asynchronously with the specified username and password.
  /// </summary>
  /// <param name="userName">The username for the new user.</param>
  /// <param name="password">The password for the new user.</param>
  /// <returns>A task that represents the asynchronous operation. The task result contains
  /// a <see cref="Result"/> indicating the success or failure of the operation,
  /// and a string representing the user ID if the operation succeeds.</returns>
  Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

  /// <summary>
  /// Deletes a user asynchronously given a userId.
  /// </summary>
  /// <param name="userId">The identifier of the user to delete.</param>
  /// <returns>A task representing the asynchronous operation. The task will complete with a Result object indicating the success or failure of the deletion.</returns>
  Task<Result> DeleteUserAsync(string userId);
}
