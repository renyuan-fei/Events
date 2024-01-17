using Application.common.DTO;
using Application.common.Models;
using Application.CQRS.Activities.Commands.CreateActivity;
using Application.CQRS.Activities.Commands.DeleteActivity;
using Application.CQRS.Activities.Commands.UpdateActivity;
using Application.CQRS.Activities.Queries.GetActivity;
using Application.CQRS.Activities.Queries.GetPaginatedActivities;
using Application.CQRS.Activities.Queries.GetPaginatedActivitiesWithAttendees;
using Application.CQRS.Attendees.Commands.AddAttendee;
using Application.CQRS.Attendees.Commands.DeleteAttendee;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

/// <summary>
///   Represents a controller for handling HTTP requests related to Activity entities.
/// </summary>
public class ActivitiesController : BaseController
{
  // GET: api/Activities
  /// <summary>
  ///   Handles HTTP GET request to retrieve a list of all Activity entities.
  /// </summary>
  /// <returns>
  ///   A task that represents the asynchronous operation. The task result contains an
  ///   IActionResult with the list of all Activity entities.
  /// </returns>
  [ HttpGet ]
  public async Task<ActionResult<IEnumerable<Activity>>> GetActivities() { return Ok(); }

  /// <summary>
  ///   Retrieves a paginated list of activities.
  /// </summary>
  /// <param name="pageSize"></param>
  /// <param name="filterParams"></param>
  /// <param name="pageNumber"></param>
  /// <returns>An ActionResult containing the paginated list of activities.</returns>
  [ HttpGet("{pageNumber:int}/{pageSize:int?}") ]
  public async Task<OkObjectResult> GetPaginatedListActivities(
      int                         pageNumber,
      int?                        pageSize,
      [ FromQuery ] FilterParams? filterParams)
  {
    var result = await Mediator!.Send(new GetPaginatedActivitiesQuery
    {
        PaginatedListParams =
            new PaginatedListParams
            {
                PageNumber = pageNumber, PageSize = pageSize ?? 10
            },
        FilterParams = filterParams
    });

    return Ok(ApiResponse<PaginatedList<ActivityWithHostUserDTO>>.Success(result));
    // return Ok(result);
  }

  // GET: api/Activities/5
  /// <summary>
  ///   Handles HTTP GET request to retrieve a specific Activity entity by its ID.
  /// </summary>
  /// <param name="id">The GUID identifier of the activity.</param>
  /// <returns>
  ///   A task that represents the asynchronous operation. The task result contains an
  ///   IActionResult with the specified Activity entity.
  /// </returns>
  [ HttpGet("{id}") ]
  public async Task<OkObjectResult> GetActivity(string id)
  {
    var result = await Mediator!.Send(new GetActivityWithAttendeesByIdQuery { Id = id });

    return Ok(ApiResponse<ActivityWithAttendeeDTO>.Success(result));
    // return Ok(result);
  }

  // PUT: api/Activities/5
  // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
  /// <summary>
  ///   Handles HTTP PUT request to update a specified Activity entity.
  /// </summary>
  /// <param name="id">The GUID identifier of the activity.</param>
  /// <param name="activity">The updated Activity entity.</param>
  /// <returns>
  ///   A task that represents the asynchronous operation. The task result is an IActionResult
  ///   representing the status of the operation.
  /// </returns>
  [ Authorize ]
  // [ Authorize(Policy = "IsActivityHost") ]
  [ HttpPut("{id}") ]
  public async Task<OkObjectResult> PutActivity(
      string                   id,
      [ FromBody ] ActivityDTO activity)
  {
    var result = await Mediator!.Send(new UpdateActivityCommand
    {
        Id = id, Activity = activity
    });

    return Ok(ApiResponse<Result>.Success(result));
    // return Ok(result);
  }

  // POST: api/Activities
  // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
  /// <summary>
  ///   Handles HTTP POST request to create a new Activity entity.
  /// </summary>
  /// <param name="activity">The new Activity entity to create.</param>
  /// <returns>
  ///   A task that represents the asynchronous operation. The task result is an IActionResult
  ///   representing the status of the operation.
  /// </returns>
  [ Authorize ]
  [ HttpPost ]
  public async Task<IActionResult> PostActivity([ FromBody ] ActivityDTO activity)
  {
    var result = await Mediator!.Send(new CreateActivityCommand
    {
        ActivityDTO = activity,
        CurrentUserId = CurrentUserService!.Id!
    });

    return Ok(ApiResponse<Result>.Success(result));
    // return Ok(result);
  }

  // DELETE: api/Activities/5
  /// <summary>
  ///   Handles HTTP DELETE request to delete a specified Activity entity.
  /// </summary>
  /// <param name="id">The GUID identifier of the activity.</param>
  /// <returns>
  ///   A task that represents the asynchronous operation. The task result is an IActionResult
  ///   representing the status of the operation.
  /// </returns>
  [ Authorize ]
  [ Authorize(Policy = "IsActivityHost") ]
  [ HttpDelete("{id}") ]
  public async Task<IActionResult> DeleteActivity(string id)
  {
    var result = await Mediator!.Send(new DeleteActivityCommand { Id = id });

    return Ok(ApiResponse<Result>.Success());
  }

  /// <summary>
  ///   Updates the attendees for a specific activity.
  /// </summary>
  /// <param name="activityId"></param>
  /// <param name="userId"></param>
  /// <returns>An IActionResult representing the status of the update.</returns>
  [ Authorize ]
  [ HttpDelete("{activityId}/attendees/{userId}") ]
  public async Task<IActionResult> DeleteAttendee(string activityId, string userId)
  {
    var result =
        await Mediator!.Send(new RemoveAttendeeCommand
        {
            ActivityId = activityId, UserId = userId
        });

    return Ok(ApiResponse<Result>.Success(result));
  }

  [ Authorize ]
  [ HttpDelete("{activityId}/attendees/{userId}") ]
  public async Task<IActionResult> AddAttendee(string activityId, string userId)
  {
    var result =
        await Mediator!.Send(new AddAttendeeCommand
        {
            ActivityId = activityId, UserId = userId
        });

    return Ok(ApiResponse<Result>.Success(result));
  }
}
