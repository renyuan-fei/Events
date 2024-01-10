using Application.common.Interfaces;
using Application.common.Models;

using Domain.ValueObjects;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Photos.Commands.DeletePhoto;

public record DeletePhotoCommand : IRequest<Result>
{
  public string UserId   { get; init; }
  public string PublicId { get; init; }
}

public class DeletePhotoHandler : IRequestHandler<DeletePhotoCommand, Result>
{
  private readonly ILogger<DeletePhotoHandler> _logger;
  private readonly IPhotoService               _photoService;

  public DeletePhotoHandler(
      ILogger<DeletePhotoHandler> logger,
      IPhotoService               photoService)
  {
    _logger = logger;
    _photoService = photoService;
  }

  public async Task<Result> Handle(
      DeletePhotoCommand request,
      CancellationToken  cancellationToken)
  {
    try
    {
      return await _photoService.RemovePhotoAsync(request.PublicId,
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
