using Application.common.Interfaces;
using Application.Common.Interfaces;
using Application.common.Models;

using AutoMapper;

using Domain.Entities;
using Domain.ValueObjects;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Photos.Commands.UpdatePhoto;

public record UpdatePhotoCommand : IRequest<Result>
{
  public string   UserId   { get; init; }
  public string PublicId { get; init; }
}

public class UpdatePhotoHandler : IRequestHandler<UpdatePhotoCommand, Result>
{
  private readonly IPhotoService               _photoService;
  private readonly ILogger<UpdatePhotoHandler> _logger;

  public UpdatePhotoHandler(
      IEventsDbContext            context,
      IMapper                     mapper,
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
      return await _photoService.UpdatePhotoAsync(request.PublicId, new UserId(request
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
