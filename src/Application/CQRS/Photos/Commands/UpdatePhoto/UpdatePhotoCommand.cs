using Application.common.Interfaces;
using Application.common.Models;

using Domain.ValueObjects;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Photos.Commands.UpdatePhoto;

public record UpdatePhotoCommand : IRequest<Result>
{
  public string UserId   { get; init; }
  public string PublicId { get; init; }
}

public class UpdatePhotoHandler : IRequestHandler<UpdatePhotoCommand, Result>
{
  private readonly ILogger<UpdatePhotoHandler> _logger;
  private readonly IPhotoService               _photoService;

  public UpdatePhotoHandler(
      ILogger<UpdatePhotoHandler> logger,
      IPhotoService               photoService)
  {
    _logger = logger;
    _photoService = photoService;
  }

  public async Task<Result> Handle(
      UpdatePhotoCommand request,
      CancellationToken  cancellationToken)
  {
    try
    {
      return await _photoService.UpdatePhotoAsync(request.PublicId,
                                                  new UserId(request
                                                      .UserId));
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
