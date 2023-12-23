using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Application.common.DTO;
using Application.common.interfaces;
using Application.CQRS.Users.Command;
using Application.CQRS.Users.Queries.GetUser;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public class UsersController : BaseController
{
  private readonly ICurrentUserService _currentUserService;

  public UsersController(ICurrentUserService currentUserService)
  {
    _currentUserService = currentUserService;
  }

  [ HttpGet ]
  public async Task<IActionResult> GetUser()
  {
    var result =
        await Mediator!.Send(new GetUserQuery()
        {
            UserId = (Guid)CurrentUserService!.UserId!
        });

    return Ok(result);
  }

  // GET: api/Users/5
  [ HttpGet("{id}") ]
  public async Task<IActionResult> GetUser(Guid id)
  {
    var result = await Mediator!.Send(new GetUserQuery() { UserId = id });

    return Ok(result);
  }

  // POST: api/Users
  [ HttpPost ]
  public void Post([ FromBody ] string value) { }

  [ Authorize ]
  [ HttpPut ]
  public async Task<IActionResult> Put([ FromBody ] UserDTO user)
  {
    if (ModelState.IsValid == false) { return BadRequest(ModelState); }

    var result = await Mediator!.Send(new UpdateUserCommand()
    {
        UserId = (Guid)_currentUserService.UserId!,
        user = user
    });

    return Ok(result);
  }

  // DELETE: api/Users/5
  [ HttpDelete("{id}") ]
  public void Delete(int id) { }
}
