using Application.common.DTO;
using Application.common.Models;
using Application.CQRS.Activities.Commands.CreateActivity;
using Application.CQRS.Activities.Commands.UpdateActivity;
using Application.CQRS.Activities.Queries.GetActivity;
using Application.CQRS.Activities.Queries.GetPaginatedActivitiesWithAttendees;

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
  public async Task<ActionResult<IEnumerable<Activity>>> GetPaginatedListActivities(
      int                         pageNumber,
      int?                        pageSize,
      [ FromQuery ] FilterParams? filterParams)
  {
    var result = await Mediator!.Send(new GetPaginatedListActivitiesWithAttendeesQuery
    {
        PaginatedListParams =
            new PaginatedListParams
            {
                PageNumber = pageNumber, PageSize = pageSize ?? 10
            },
        FilterParams = filterParams
    });

    return Ok(result);
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
  public async Task<ActionResult<Activity>> GetActivity(string id)
  {
    var result = await Mediator!.Send(new GetActivityWithAttendeesByIdQuery { Id = id });

    return Ok(result);
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
  public async Task<IActionResult> PutActivity(
      string                   id,
      [ FromBody ] ActivityDTO activity)
  {
    var result = await Mediator!.Send(new UpdateActivityCommand
    {
        Id = id, Activity = activity
    });

    return Ok(result);
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

    return Ok(result);
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
  [ HttpDelete("{id:guid}") ]
  public async Task<IActionResult> DeleteActivity(Guid id) { return Ok(); }

  /// <summary>
  ///   Updates the attendees for a specific activity.
  /// </summary>
  /// <param name="id">The ID of the activity.</param>
  /// <returns>An IActionResult representing the status of the update.</returns>
  [ Authorize ]
  [ HttpPut("{id:guid}/attendees") ]
  public async Task<IActionResult> UpdateActivityAttendees(Guid id) { return Ok(); }
}
