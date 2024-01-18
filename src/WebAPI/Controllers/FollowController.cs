using Application.common.DTO;
using Application.common.Models;
using Application.CQRS.Followers.Commands.UpdateFollowingCommand;
using Application.CQRS.Followers.Queries;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

/// <summary>
///   Controller class for managing follow operations.
/// </summary>
public class FollowController : BaseController
{
  // GET: api/Follow/5
  /// <summary>
  ///   Retrieves a paginated list of followers for the current user.
  /// </summary>
  /// <returns>A paginated list of followers.</returns>
  [ Authorize ]
  [ HttpGet("Follower/{pageNumber:int}/{pageSize:int?}") ]
  public async Task<IActionResult> GetFollower(int pageNumber, int? pageSize)
  {
    var result = await Mediator!.Send(new GetFollowerQuery
    {
        UserId = CurrentUserService!.Id!,
        PaginatedListParams =
            new PaginatedListParams { PageNumber = pageNumber, PageSize = pageSize ?? 10 }
    });

    return Ok(ApiResponse<PaginatedList<FollowerDTO>>.Success(result));
  }

  /// <summary>
  ///   Retrieves a paginated list of followee for the current user.
  /// </summary>
  /// <returns>A paginated list of followee.</returns>
  [ Authorize ]
  [ HttpGet("Following/{pageNumber:int}/{pageSize:int?}") ]
  public async Task<IActionResult> GetFollowing(int pageNumber, int? pageSize)
  {
    var result = await Mediator!.Send(new GetFollowingQuery
    {
        UserId = CurrentUserService!.Id!,
        PaginatedListParams =
            new PaginatedListParams { PageNumber = pageNumber, PageSize = pageSize ?? 10 }
    });

    return Ok(ApiResponse<PaginatedList<FollowingDTO>>.Success(result));
  }

  // PUT: api/Follow/5
  [ Authorize ]
  [ HttpPut("{id}") ]
  public async Task<IActionResult> UpdateFollowing(string id)
  {
    var result = await Mediator!.Send(new UpdateFollowerCommand
    {
        FollowingId = id,
        FollowerId = CurrentUserService!.Id!
    });

    return Ok(ApiResponse<Result>.Success(data: result));
  }
}
