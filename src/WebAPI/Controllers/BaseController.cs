using Application.common.interfaces;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

/// <summary>
///   Custom controller base class providing common functionality and access to the Mediator.
/// </summary>
[ Route("api/[controller]") ]
[ ApiController ]
public class BaseController : ControllerBase
{
  /// <summary>
  ///   Represents an instance of the mediator used for handling communication between objects.
  /// </summary>
  private IMediator? _mediator;

  private ICurrentUserService? _currentUserService;

  /// <summary>
  ///   Gets the Mediator instance.
  /// </summary>
  /// <remarks>
  ///   This property returns the instance of the Mediator, which is responsible for handling
  ///   communication and coordination between different components.
  /// </remarks>
  protected IMediator? Mediator => _mediator ??=
      HttpContext.RequestServices.GetService<IMediator>();

  /// <summary>
  /// Represents the current user service used to retrieve information about the currently authenticated user.
  /// </summary>
  protected ICurrentUserService? CurrentUserService => _currentUserService ??=
      HttpContext.RequestServices.GetService<ICurrentUserService>();
}
