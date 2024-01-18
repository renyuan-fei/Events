using Application.common.interfaces;
using Application.common.Models;
using Application.CQRS.Photos.Commands.CreatePhoto;
using Application.CQRS.Photos.Commands.DeletePhoto;
using Application.CQRS.Photos.Commands.UpdatePhoto;

using Domain.ValueObjects;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

/// <summary>
///   Controller for handling photo-related operations.
/// </summary>
public class PhotosController : BaseController
{
  // POST: api/Photo
  /// <summary>
  ///   Uploads a photo.
  /// </summary>
  /// <param name="file">The photo file to upload.</param>
  /// <returns>The result of the photo upload.</returns>
  /// <remarks>
  ///   This method uploads a photo specified by the <paramref name="file" /> parameter.
  ///   The uploaded photo is processed by the <see cref="Mediator" /> to create a new photo
  ///   using the <see cref="UploadUserPhotoCommand" /> command.
  ///   The <see cref="UserId" /> property is set to the current user's ID obtained from the
  ///   <see cref="ICurrentUserService" />.
  ///   The result of the photo upload is returned as an <see cref="IActionResult" /> of the
  ///   form <see>
  ///     <cref>Ok(result)</cref>
  ///   </see>
  ///   .
  /// </remarks>
  [ HttpPost ]
  [ Authorize ]
  public async Task<IActionResult> UpLoadUserPhoto([ FromForm ] IFormFile file)
  {
    var result = await Mediator?.Send(new UploadUserPhotoCommand
    {
        UserId = CurrentUserService!.Id!,
        File = file
    })!;

    return CreatedAtAction("", new { }, ApiResponse<Result>.Success(data: result,
        statusCode: StatusCodes.Status201Created));
  }

  [ HttpPost("{id}") ]
  [ Authorize ]
  [ Authorize(Policy = "IsActivityHost") ]
  public async Task<IActionResult> UpLoadActivityPhoto(
      [ FromForm ] IFormFile file,
      string                 id)
  {
    var result = await Mediator?.Send(new UploadActivityPhotoCommand
    {
        UserId = CurrentUserService!.Id!,
        ActivityId = id,
        File = file
    })!;

    return CreatedAtAction("", new { }, ApiResponse<Result>.Success(data: result,
                             statusCode: StatusCodes.Status201Created));
  }

  // PUT: api/Photo/5
  /// <summary>
  ///   Updates a photo with the given id.
  /// </summary>
  /// <param name="id">The public id of the photo to be updated.</param>
  /// <returns>An IActionResult representing the updated photo.</returns>
  [ HttpPut("{id:required}") ]
  [ Authorize ]
  public async Task<IActionResult> UpdatePhoto(string id)
  {
    var result = await Mediator?.Send(new UpdatePhotoCommand
    {
        UserId = CurrentUserService!.Id!,
        PublicId = id
    })!;

    return Ok(ApiResponse<Result>.Success(data: result));
  }

  // DELETE: api/Photo/5
  /// <summary>
  ///   Deletes a photo.
  /// </summary>
  /// <param name="id">The public ID of the photo to delete.</param>
  /// <returns>An <see cref="IActionResult" /> representing the result of the operation.</returns>
  [ HttpDelete("{id:required}") ]
  [ Authorize ]
  public async Task<IActionResult> DeletePhoto(string id)
  {
    var result = await Mediator?.Send(new DeletePhotoCommand
    {
        UserId = CurrentUserService!.Id!,
        PublicId = id
    })!;

    return Ok(ApiResponse<Result>.Success(data: result));
  }
}
