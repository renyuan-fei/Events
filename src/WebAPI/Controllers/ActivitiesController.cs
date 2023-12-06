using Application.CQRS.Activities.Commands.CreateActivity;
using Application.CQRS.Activities.Commands.DeleteActivity;
using Application.CQRS.Activities.Commands.UpdateActivity;
using Application.CQRS.Activities.Queries.GetActivity;

using Domain.Entities;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

/// <summary>
/// Represents a controller for handling HTTP requests related to Activity entities.
/// </summary>
[Authorize]
public class ActivitiesController : BaseController
{
  // GET: api/Activities
  /// <summary>
  /// Handles HTTP GET request to retrieve a list of all Activity entities.
  /// </summary>
  /// <returns>
  /// A task that represents the asynchronous operation. The task result contains an IActionResult with the list of all Activity entities.
  /// </returns>
  [ HttpGet ]
  public async Task<ActionResult<IEnumerable<Activity>>> GetActivities()
  {
    var result = await Mediator!.Send(new GetAllActivitiesQuery());

    return Ok(result);
  }

  // GET: api/Activities/5
  /// <summary>
  /// Handles HTTP GET request to retrieve a specific Activity entity by its ID.
  /// </summary>
  /// <param name="id">The GUID identifier of the activity.</param>
  /// <returns>
  /// A task that represents the asynchronous operation. The task result contains an IActionResult with the specified Activity entity.
  /// </returns>
  [ HttpGet("{id}") ]
  public async Task<ActionResult<Activity>> GetActivity(Guid id)
  {
    var result = await Mediator!.Send(new GetActivityByIdQuery { Id = id });

    return Ok(result);
  }

  // PUT: api/Activities/5
  // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
  /// <summary>
  /// Handles HTTP PUT request to update a specified Activity entity.
  /// </summary>
  /// <param name="id">The GUID identifier of the activity.</param>
  /// <param name="activity">The updated Activity entity.</param>
  /// <returns>
  /// A task that represents the asynchronous operation. The task result is an IActionResult representing the status of the operation.
  /// </returns>
  [ HttpPut("{id}") ]
  public async Task<IActionResult> PutActivity(Guid id, Activity activity)
  {
    var result =
        await Mediator!.Send(new UpdateActivityCommand { Id = id, Activity = activity });

    return Ok(result);
  }

  // POST: api/Activities
  // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
  /// <summary>
  /// Handles HTTP POST request to create a new Activity entity.
  /// </summary>
  /// <param name="activity">The new Activity entity to create.</param>
  /// <returns>
  /// A task that represents the asynchronous operation. The task result is an IActionResult representing the status of the operation.
  /// </returns>
  [ HttpPost ]
  public async Task<IActionResult> PostActivity(Activity activity)
  {
    var result =
        await Mediator!.Send(new CreateActivityCommand { Activity = activity });

    return Ok(result);
  }

  // DELETE: api/Activities/5
  /// <summary>
  /// Handles HTTP DELETE request to delete a specified Activity entity.
  /// </summary>
  /// <param name="id">The GUID identifier of the activity.</param>
  /// <returns>
  /// A task that represents the asynchronous operation. The task result is an IActionResult representing the status of the operation.
  /// </returns>
  [ HttpDelete("{id}") ]
  public async Task<IActionResult> DeleteActivity(Guid id)
  {
    var result = await Mediator!.Send(new DeleteActivityCommand { Id = id });

    return Ok(result);
  }

  // private bool ActivityExists(Guid id) { throw new NotImplementedException(); }
}
