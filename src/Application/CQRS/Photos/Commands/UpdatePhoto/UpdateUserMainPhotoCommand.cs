using Application.common.Interfaces;
using Application.common.Models;

using Domain.ValueObjects;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Photos.Commands.UpdatePhoto;

public record UpdateUserMainPhotoCommand : IRequest<Result>
{
  public string    UserId { get; init; }
  public IFormFile File   { get; init; }

}

public class UpdateUserMainPhotoCommandHandler : IRequestHandler<UpdateUserMainPhotoCommand, Result>
{
  private readonly ILogger<UpdateUserMainPhotoCommandHandler> _logger;
  private readonly IPhotoService               _photoService;

  public UpdateUserMainPhotoCommandHandler(
      ILogger<UpdateUserMainPhotoCommandHandler> logger,
      IPhotoService               photoService)
  {
    _logger = logger;
    _photoService = photoService;
  }

  public async Task<Result> Handle(
      UpdateUserMainPhotoCommand request,
      CancellationToken  cancellationToken)
  {
    try
    {
      return await _photoService.UpdateMainPhotoAsync(request.File,
                                                  request
                                                      .UserId);
    }
    catch (Exception ex)
    {
      _logger.LogError(ex,
                       "ErrorMessage saving to the database: {ExMessage}",
                       ex.Message);

      throw;
    }
  }
}
