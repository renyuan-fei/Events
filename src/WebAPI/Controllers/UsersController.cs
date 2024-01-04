using Application.common.DTO;
using Application.common.interfaces;

using Microsoft.AspNetCore.Authorization;
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
  public async Task<IActionResult> GetUser() { throw new NotImplementedException(); }

  // GET: api/Users/5
  [ HttpGet("{id}") ]
  public async Task<IActionResult> GetUser(Guid id)
  {
    throw new NotImplementedException();
  }

  // POST: api/Users
  [ HttpPost ]
  public void Post([ FromBody ] string value) { }

  [ Authorize ]
  [ HttpPut ]
  public async Task<IActionResult> Put([ FromBody ] UserDTO user)
  {
    throw new NotImplementedException();
  }

  // DELETE: api/Users/5
  [ HttpDelete("{id}") ]
  public void Delete(int id) { }
}
