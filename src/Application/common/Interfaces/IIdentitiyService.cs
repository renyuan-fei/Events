using Application.common.Models;

namespace Application.common.interfaces;

/// <summary>
/// Represents an identity service for managing user identities.
/// </summary>
public interface IIdentityService
{
  /// <summary>
  /// Retrieves the user name asynchronously for the specified user ID.
  /// </summary>
  /// <param name="userId">The GUID representing the user ID.</param>
  /// <returns>A task that represents the asynchronous operation. The task result contains the user name as a nullable string. If the user name is not found, null is returned.</returns>
  /// <remarks>
  /// This method asynchronously fetches the user name from the database based on the provided user ID. If the user name is found, it is returned as a string. If the user name is not found
  /// , null is returned.
  /// </remarks>
  Task<string?> GetUserNameAsync(Guid userId);

  /// <summary>
  /// Determines whether the user with the specified id is in the given role asynchronously.
  /// </summary>
  /// <param name="userId">The unique identifier of the user.</param>
  /// <param name="role">The role name to check.</param>
  /// <returns>
  /// A task that represents the asynchronous operation. The task result contains a boolean value
  /// indicating whether the user is in the given role.
  /// </returns>
  Task<bool> IsInRoleAsync(Guid userId, string role);

  /// <summary>
  /// Authorizes the specified user against the given policy.
  /// </summary>
  /// <param name="userId">The unique identifier of the user.</param>
  /// <param name="policyName">The name of the policy to authorize against.</param>
  /// <returns>Returns a task that represents the asynchronous authorization operation. The task result is true if the user is authorized; otherwise, false.</returns>
  Task<bool> AuthorizeAsync(Guid userId, string policyName);

  /// <summary>
  /// Creates a new user asynchronously.
  /// </summary>
  /// <param name="userName">The username of the user.</param>
  /// <param name="password">The password of the user.</param>
  /// <returns>A task that represents the asynchronous operation. The task result contains a tuple with an enumeration Result value and the user ID as a Guid.</returns>
  /// <remarks>
  /// The <paramref name="userName"/> parameter should contain a valid username. It must not be null or empty.
  /// The <paramref name="password"/> parameter should contain a valid password. It must not be null or empty.
  /// </remarks>
  Task<(Result Result, Guid userId)> CreateUserAsync(
      string userName,
      string
          password);

  /// <summary>
  /// Asynchronously deletes a user with the specified userId.
  /// </summary>
  /// <param name="userId">The unique identifier of the user to delete.</param>
  /// <returns>A task representing the asynchronous operation. The result of the task will contain a Result object indicating the success or failure of the delete operation.</returns>
  Task<Result> DeleteUserAsync(Guid userId);
}
