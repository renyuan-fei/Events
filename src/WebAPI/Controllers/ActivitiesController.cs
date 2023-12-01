using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Application.CQRS.Activities;
using Application.CQRS.Activities.Commands.CreateActivity;
using Application.CQRS.Activities.Commands.DeleteActivity;
using Application.CQRS.Activities.Commands.UpdateActivity;
using Application.CQRS.Activities.Queries;
using Application.CQRS.Activities.Queries.GetActivity;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Domain.Entities;

using Infrastructure.DatabaseContext;

using MediatR;

namespace WebAPI.Controllers
{

  /// <summary>
  /// Activities controller
  /// </summary>
  public class ActivitiesController : BaseController
  {
    // GET: api/Activities
    /// <summary>
    /// Get all activities
    /// </summary>
    /// <returns></returns>
    [ HttpGet ]
    public async Task<ActionResult<IEnumerable<Activity>>> GetActivities()
    {
      var result = await Mediator!.Send(new GetAllActivitiesQuery());

      return Ok(result);
    }

    // GET: api/Activities/5
    /// <summary>
    /// Get a specific activity in the database by its ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [ HttpGet("{id}") ]
    public async Task<ActionResult<Activity>> GetActivity(Guid id)
    {
      var result = await Mediator!.Send(new GetActivityByIdQuery { Id = id });

      return Ok(result);
    }

    // PUT: api/Activities/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Update the specified activity in the database
    /// </summary>
    /// <param name="id"></param>
    /// <param name="activity"></param>
    /// <returns></returns>
    [ HttpPut("{id}") ]
    public async Task<IActionResult> PutActivity(Guid id, Activity activity)
    {
      var result =
          await Mediator!.Send(new UpdateActivityCommand
          {
              Id = id, Activity = activity
          });

      return HandleCommandResult(result);
    }

    // POST: api/Activities
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Add a new activity in the database
    /// </summary>
    /// <param name="activity"></param>
    /// <returns></returns>
    [ HttpPost ]
    public async Task<IActionResult> PostActivity(Activity activity)
    {
      var result =
          await Mediator!.Send(new CreateActivityCommand { Activity = activity });

      return HandleCommandResult(result);
    }

    // DELETE: api/Activities/5
    /// <summary>
    /// Delete an activity in the database by its id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [ HttpDelete("{id}") ]
    public async Task<IActionResult> DeleteActivity(Guid id)
    {
      var result = await Mediator!.Send(new DeleteActivityCommand { Id = id });

      return HandleCommandResult(result);
    }

    private bool ActivityExists(Guid id) { throw new NotImplementedException(); }
  }

}
