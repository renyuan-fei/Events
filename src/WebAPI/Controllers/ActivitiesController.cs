using Application.common.interfaces;
using Application.common.Models;
using Application.CQRS.Activities.Commands.CreateActivity;
using Application.CQRS.Activities.Commands.DeleteActivity;
using Application.CQRS.Activities.Commands.UpdateActivity;
using Application.CQRS.Activities.Queries.GetActivity;
using Application.CQRS.ActivityAttendee.Commands.UpdateActivityAttendee;

using Domain.Entities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

/// <summary>
///   Represents a controller for handling HTTP requests related to Activity entities.
/// </summary>
[ Authorize ]
public class ActivitiesController : BaseController
{
  private readonly ICurrentUserService _currentUserService;

  /// <summary>
  ///   Initializes a new instance of the ActivitiesController class.
  /// </summary>
  /// <param name="currentUserService">
  ///   The service for retrieving information about the current
  ///   user.
  /// </param>
  public ActivitiesController(ICurrentUserService currentUserService)
  {
    _currentUserService = currentUserService;
  }

  // GET: api/Activities
  /// <summary>
  ///   Handles HTTP GET request to retrieve a list of all Activity entities.
  /// </summary>
  /// <returns>
  ///   A task that represents the asynchronous operation. The task result contains an
  ///   IActionResult with the list of all Activity entities.
  /// </returns>
  [ HttpGet ]
  public async Task<ActionResult<IEnumerable<Activity>>> GetActivities()
  {
    var result = await Mediator!.Send(new GetAllActivitiesQuery());

    return Ok(result);
  }

  /// <summary>
  /// Retrieves a paginated list of activities.
  /// </summary>
  /// <param name="page">The page number to retrieve.</param>
  /// <param name="paginatedListParams">The parameters for the paginated list.</param>
  /// <returns>An ActionResult containing the paginated list of activities.</returns>
  [ HttpGet("{page:int}/{searchTerm?}") ]
  public async Task<ActionResult<IEnumerable<Activity>>> GetPaginatedListActivities(
      int                               page,
      [ FromQuery ] PaginatedListParams paginatedListParams)
  {
    paginatedListParams.PageNumber = page;

    var result = await Mediator!.Send(new GetPaginatedListActivitiesQuery
    {
        PaginatedListParams = paginatedListParams
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
  [ HttpGet("{id:guid}") ]
  public async Task<ActionResult<Activity>> GetActivity(Guid id)
  {
    var result = await Mediator!.Send(new GetActivityByIdQuery { Id = id });

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
  [ Authorize(Policy = "IsActivityHost") ]
  [ HttpPut("{id:guid}") ]
  public async Task<IActionResult> PutActivity(Guid id, [ FromBody ] Activity activity)
  {
    var result =
        await Mediator!.Send(new UpdateActivityCommand { Id = id, Activity = activity });

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
  [ HttpPost ]
  public async Task<IActionResult> PostActivity([ FromBody ] Activity activity)
  {
    var result =
        await Mediator!.Send(new CreateActivityCommand
        {
            Activity = activity,
            CurrentUserId = (Guid)_currentUserService.UserId!
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
  [ HttpDelete("{id:guid}") ]
  public async Task<IActionResult> DeleteActivity(Guid id)
  {
    var result = await Mediator!.Send(new DeleteActivityCommand { Id = id });

    return Ok(result);
  }

  /// <summary>
  ///   Updates the attendees for a specific activity.
  /// </summary>
  /// <param name="id">The ID of the activity.</param>
  /// <returns>An IActionResult representing the status of the update.</returns>
  [ HttpPut("{id:guid}/attendees") ]
  public async Task<IActionResult> UpdateActivityAttendees(Guid id)
  {
    var result = await Mediator!.Send(new UpdateActivityAttendeeCommand
    {
        Id = (Guid)_currentUserService.UserId!,
        ActivityId = id
    });

    return Ok(result);
  }
  // private bool ActivityExists(Guid id) { throw new NotImplementedException(); }
}
