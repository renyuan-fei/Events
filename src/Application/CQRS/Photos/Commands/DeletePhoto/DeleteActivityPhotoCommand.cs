using Application.common.Interfaces;
using Application.common.Models;

using AutoMapper;

using Domain.Entities;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Photos.Commands.DeletePhoto;

public record DeleteActivityPhotoCommand : IRequest<Result>
{
  public string ActivityId { get; init; }
  public string PublicId { get; init; }
}

public class
    DeleteActivityPhotoCommandHandler : IRequestHandler<DeleteActivityPhotoCommand,
    Result>
{
  private readonly IPhotoService                              _photoService;
  private readonly ILogger<DeleteActivityPhotoCommandHandler> _logger;

  public DeleteActivityPhotoCommandHandler(
      ILogger<DeleteActivityPhotoCommandHandler> logger,
      IPhotoService                              photoService)
  {
    _logger = logger;
    _photoService = photoService;
  }

  public async Task<Result> Handle(
      DeleteActivityPhotoCommand request,
      CancellationToken          cancellationToken)
  {
    try
    {
      return await _photoService.RemovePhotoAsync(request.PublicId,
                                                  request
                                                      .ActivityId);
    }
    catch (Exception ex)
    {
      _logger.LogError(ex,
                       "Error occurred in {Name}: {ExMessage}",
                       nameof(DeleteActivityPhotoCommand),
                       ex.Message);

      throw;
    }
  }
}
