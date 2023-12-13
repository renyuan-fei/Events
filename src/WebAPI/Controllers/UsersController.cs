using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Application.common.DTO;
using Application.common.interfaces;
using Application.CQRS.Users.Queries.GetUser;

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

  // PUT: api/Users/5
  [ HttpPut("{id}") ]
  public void Put(int id, [ FromBody ] string value) { }

  // DELETE: api/Users/5
  [ HttpDelete("{id}") ]
  public void Delete(int id) { }
}