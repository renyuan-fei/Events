using Application.common.DTO;
using Application.common.interfaces;
using Application.common.Models;
using Application.CQRS.Users.Command.UpdateUser;
using Application.CQRS.Users.Queries.GetUser;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public class UsersController : BaseController
{
  [ Authorize ]
  [ HttpGet ]
  public async Task<IActionResult> GetUser()
  {
    var result = await Mediator!.Send(new GetUserQuery { UserId = CurrentUserService!.Id! });

    return Ok(ApiResponse<UserProfileDto>.Success(result));
  }

  // GET: api/Users/5
  [ HttpGet("{id:required}") ]
  public async Task<IActionResult> GetUser(string id)
  {
    var result = await Mediator!.Send(new GetUserQuery { UserId = id });

    return Ok(ApiResponse<UserProfileDto>.Success(result));
  }

  [ Authorize ]
  [ HttpPut ]
  public async Task<IActionResult> Put([ FromBody ] UserDto user)
  {
    var result = await Mediator!.Send(new UpdateUserCommand
    {
        UserId = CurrentUserService!.Id!,
        User = user
    });

    return Ok(ApiResponse<UserDto>.Success(statusCode: StatusCodes.Status200OK, message: "User updated successfully."));
  }

  // DELETE: api/Users/5
  [ HttpDelete("{id}") ]
  public void Delete(int id) { }
}
