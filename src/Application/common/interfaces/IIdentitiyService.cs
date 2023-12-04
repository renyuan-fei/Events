using Application.common.Models;

namespace Application.common.interfaces;

public interface IIdentityService
{
  Task<string?> GetUserNameAsync(Guid userId);

  Task<bool> IsInRoleAsync(Guid userId, string role);

  Task<bool> AuthorizeAsync(Guid userId, string policyName);

  Task<(Result Result, Guid userId)> CreateUserAsync(
      string userName,
      string
          password);

  Task<Result> DeleteUserAsync(Guid userId);
}
