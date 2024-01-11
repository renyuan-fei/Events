using Application.common.Interfaces;
using Application.common.Models;

using Domain.ValueObjects;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Photos.Commands.CreatePhoto;

public record UploadUserPhotoCommand : IRequest<Result>
{
  public string    UserId { get; init; }
  public IFormFile File   { get; init; }
}

public class CreatePhotoHandler : IRequestHandler<UploadUserPhotoCommand, Result>
{
  private readonly ILogger<CreatePhotoHandler> _logger;
  private readonly IPhotoService               _photoService;

  public CreatePhotoHandler(
      ILogger<CreatePhotoHandler> logger,
      IPhotoService               photoService)
  {
    _logger = logger;
    _photoService = photoService;
  }

  public async Task<Result> Handle(
      UploadUserPhotoCommand request,
      CancellationToken  cancellationToken)
  {
    try
    {
      return await _photoService.AddPhotoAsync(request.File, request.UserId);
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
