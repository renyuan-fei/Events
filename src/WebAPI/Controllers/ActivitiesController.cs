using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Domain.Entities;

using Infrastructure.DatabaseContext;

namespace WebAPI.Controllers
{

  /// <summary>
  /// Activities controller
  /// </summary>
  public class ActivitiesController : BaseController
  {
    private readonly ApplicationDbContext          _context;
    private readonly ILogger<ActivitiesController> _logger;

    /// <summary>
    /// Constructor For DI
    /// </summary>
    /// <param name="context"></param>
    /// <param name="logger"></param>
    public ActivitiesController(
        ApplicationDbContext          context,
        ILogger<ActivitiesController> logger)
    {
      _context = context;
      _logger = logger;
    }

    // GET: api/Activities
    /// <summary>
    /// Get all activities
    /// </summary>
    /// <returns></returns>
    [ HttpGet ]
    public async Task<ActionResult<IEnumerable<Activity>>> GetActivities()
    {
      if (_context.Activities == null) { return NotFound(); }

      return await _context.Activities.ToListAsync();
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
      if (_context.Activities == null) { return NotFound(); }

      var activity = await _context.Activities.FindAsync(id);

      if (activity == null) { return NotFound(); }

      return activity;
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
      if (id != activity.Id) { return BadRequest(); }

      _context.Entry(activity).State = EntityState.Modified;

      try { await _context.SaveChangesAsync(); }
      catch (DbUpdateConcurrencyException)
      {
        if (!ActivityExists(id)) { return NotFound(); }
        else { throw; }
      }

      return NoContent();
    }

    // POST: api/Activities
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// <summary>
    /// Add a new activity in the database
    /// </summary>
    /// <param name="activity"></param>
    /// <returns></returns>
    [ HttpPost ]
    public async Task<ActionResult<Activity>> PostActivity(Activity activity)
    {
      if (_context.Activities == null)
      {
        return Problem("Entity set 'ApplicationDbContext.Activities'  is null.");
      }

      _context.Activities.Add(activity);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetActivity", new { id = activity.Id }, activity);
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
      if (_context.Activities == null) { return NotFound(); }

      var activity = await _context.Activities.FindAsync(id);
      if (activity == null) { return NotFound(); }

      _context.Activities.Remove(activity);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool ActivityExists(Guid id)
    {
      return (_context.Activities?.Any(e => e.Id == id)).GetValueOrDefault();
    }
  }

}
