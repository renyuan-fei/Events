using Application.common.DTO;
using Application.common.Models;
using Application.CQRS.Followers.Commands.CreateFollowingCommand;
using Application.CQRS.Followers.Commands.DeleteFollowingCommand;
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

    return Ok(ApiResponse<PaginatedList<FollowerDto>>.Success(result));
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

  [Authorize]
  [HttpGet("IsFollowing/{targetUserId}")]
  public async Task<IActionResult> IsFollowing(string targetUserId)
  {
    var result = await Mediator!.Send(new IsFollowingQuery
    {
        UserId = CurrentUserService!.Id!,
        targetUserId = targetUserId
    });

    return Ok(ApiResponse<bool>.Success(result));
  }

  [Authorize]
  [ HttpPost ]
  public async Task<IActionResult> CreateFollowing([FromQuery]string id)
  {
    var result = await Mediator!.Send(new CreateFollowingCommand
    {
        UserId = CurrentUserService!.Id!,
        TargetUserId = id
    });

    return StatusCode(StatusCodes.Status201Created,ApiResponse<Result>.Success(data: result));
  }

  [Authorize]
  [ HttpDelete ]
  public async Task<IActionResult> DeleteFollowing([FromQuery] string id)
  {
    var result = await Mediator!.Send(new DeleteFollowingCommand
    {
        UserId = CurrentUserService!.Id!,
        TargetUserId = id
    });

    return Ok(ApiResponse<Result>.Success(data: result));
  }
}
