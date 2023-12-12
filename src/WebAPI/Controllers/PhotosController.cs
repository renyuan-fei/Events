using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Application.common.interfaces;
using Application.CQRS.Photos.Commands.CreatePhoto;
using Application.CQRS.Photos.Commands.DeletePhoto;
using Application.CQRS.Photos.Commands.UpdatePhoto;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public class PhotosController : BaseController
{
  private readonly ICurrentUserService _currentUserService;

  public PhotosController(ICurrentUserService currentUserService)
  {
    _currentUserService = currentUserService;
  }

  // POST: api/Photo
  [ HttpPost ]
  public async Task<IActionResult> UpLoadPhoto([ FromForm ] IFormFile file)
  {
    var result = await Mediator!.Send(new CreatePhoto
    {
        File = file,
        UserId = (Guid)_currentUserService.UserId!
    });

    return Ok(result);
  }

  // PUT: api/Photo/5
  [ HttpPut("{id}") ]
  public async Task<IActionResult> UpdatePhoto(string id)
  {
    var result = await Mediator!.Send(new UpdatePhoto()
    {
        PublicId = id,
        UserId = (Guid)_currentUserService.UserId!
    });

    return Ok(result);
  }

  // DELETE: api/Photo/5
  [ HttpDelete("{id}") ]
  public async Task<IActionResult> DeletePhoto(string id)
  {
    var result =
        await Mediator!.Send(new DeletePhoto
        {
            PublicId = id,
            UserId = (Guid)_currentUserService.UserId!,
        });

    return Ok(result);
  }
}
