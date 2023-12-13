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

/// <summary>
/// Controller for handling photo-related operations.
/// </summary>
public class PhotosController : BaseController
{
  // private readonly ICurrentUserService _currentUserService;
  //
  // public PhotosController(ICurrentUserService currentUserService)
  // {
  //   _currentUserService = currentUserService;
  // }

  // POST: api/Photo
  /// <summary>
  /// Uploads a photo.
  /// </summary>
  /// <param name="file">The photo file to upload.</param>
  /// <returns>The result of the photo upload.</returns>
  /// <remarks>
  /// This method uploads a photo specified by the <paramref name="file"/> parameter.
  /// The uploaded photo is processed by the <see cref="Mediator"/> to create a new photo using the <see cref="CreatePhotoCommand"/> command.
  /// The <see cref="UserId"/> property is set to the current user's ID obtained from the <see cref="ICurrentUserService"/>.
  /// The result of the photo upload is returned as an <see cref="IActionResult"/> of the form <see cref="Ok(result)"/>.
  /// </remarks>
  [ HttpPost ]
  public async Task<IActionResult> UpLoadPhoto([ FromForm ] IFormFile file)
  {
    var result = await Mediator!.Send(new CreatePhotoCommand
    {
        File = file,
        UserId = (Guid)CurrentUserService!.UserId!
    });

    return Ok(result);
  }

  // PUT: api/Photo/5
  /// <summary>
  /// Updates a photo with the given id.
  /// </summary>
  /// <param name="id">The public id of the photo to be updated.</param>
  /// <returns>An IActionResult representing the updated photo.</returns>
  [ HttpPut("{id}") ]
  public async Task<IActionResult> UpdatePhoto(string id)
  {
    var result = await Mediator!.Send(new UpdatePhotoCommand()
    {
        PublicId = id,
        UserId = (Guid)CurrentUserService!.UserId!
    });

    return Ok(result);
  }

  // DELETE: api/Photo/5
  /// <summary>
  /// Deletes a photo.
  /// </summary>
  /// <param name="id">The public ID of the photo to delete.</param>
  /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
  [ HttpDelete("{id}") ]
  public async Task<IActionResult> DeletePhoto(string id)
  {
    var result =
        await Mediator!.Send(new DeletePhotoCommand
        {
            PublicId = id,
            UserId = (Guid)CurrentUserService!.UserId!
        });

    return Ok(result);
  }
}
