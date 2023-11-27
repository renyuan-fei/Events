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

}
