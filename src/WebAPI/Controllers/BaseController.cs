using Application.common.Models;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

/// <summary>
/// Custom controller base class
/// </summary>
// [ Route("api/v{version:apiVersion}/[controller]") ]
[ Route("api/[controller]") ]
[ ApiController ]
public class BaseController : ControllerBase
{
  private IMediator? _mediator;

  /// <summary>
  /// Mediator instance
  /// </summary>
  protected IMediator? Mediator => _mediator ??=
      HttpContext.RequestServices.GetService<IMediator>();

  /// <summary>
  /// Used to handle the result of a MediatR command
  /// </summary>
  /// <param name="result"></param>
  /// <returns></returns>
  protected ActionResult HandleCommandResult(Result result)
  {
    if (result.Succeeded)
      return Ok();

    if (result.Errors.Any())
      return BadRequest(result.Errors);

    return NotFound();
  }
}
