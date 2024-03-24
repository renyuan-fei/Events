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
  [ HttpGet("Follower/{id}") ]
  public async Task<IActionResult> GetFollower(
      string id,
      [ FromQuery ] PaginatedListParams paginatedListParams)
  {
    var result = await Mediator!.Send(new GetFollowerQuery
    {
        UserId = id,
        PaginatedListParams = paginatedListParams
    });

    return Ok(ApiResponse<PaginatedList<FollowerDto>>.Success(result));
  }

  /// <summary>
  ///   Retrieves a paginated list of followee for the current user.
  /// </summary>
  /// <returns>A paginated list of followee.</returns>
  [ HttpGet("Following/{id}") ]
  public async Task<IActionResult> GetFollowing(
      string id,
      [ FromQuery ] PaginatedListParams paginatedListParams)
  {
    var result = await Mediator!.Send(new GetFollowingQuery
    {
        UserId = id,
        PaginatedListParams = paginatedListParams
    });

    return Ok(ApiResponse<PaginatedList<FollowingDto>>.Success(result));
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

  [ Authorize ]
  [ HttpGet("IsFollowing") ]
  public async Task<IActionResult> IsFollowing([ FromQuery ] string targetUserId)
  {
    var result = await Mediator!.Send(new IsFollowingQuery
    {
        UserId = CurrentUserService!.Id!,
        targetUserId = targetUserId
    });

    return Ok(ApiResponse<bool>.Success(result));
  }

  [ Authorize ]
  [ HttpPost ]
  public async Task<IActionResult> CreateFollowing([ FromQuery ] string targetUserId)
  {
    var result = await Mediator!.Send(new CreateFollowingCommand
    {
        UserId = CurrentUserService!.Id!,
        TargetUserId = targetUserId
    });

    return StatusCode(StatusCodes.Status201Created,
                      ApiResponse<Result>.Success(data: result));
  }

  [ Authorize ]
  [ HttpDelete ]
  public async Task<IActionResult> DeleteFollowing([ FromQuery ] string targetUserId)
  {
    var result = await Mediator!.Send(new DeleteFollowingCommand
    {
        UserId = CurrentUserService!.Id!,
        TargetUserId = targetUserId
    });

    return Ok(ApiResponse<Result>.Success(data: result));
  }
}
