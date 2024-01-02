using Application.common.Interfaces;
using Application.Common.Interfaces;
using Application.common.Models;

using AutoMapper;

using Domain;
using Domain.Entities;
using Domain.ValueObjects;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Photos.Commands.CreatePhoto;

public record CreatePhotoCommand : IRequest<Result>
{
  public string      UserId { get; init; }
  public IFormFile File   { get; init; }
}

public class CreatePhotoHandler : IRequestHandler<CreatePhotoCommand, Result>
{
  private readonly IPhotoService _photoService;
  private readonly ILogger<CreatePhotoHandler> _logger;

  public CreatePhotoHandler(
      ILogger<CreatePhotoHandler> logger,
      IPhotoService               photoService)
  {
    _logger = logger;
    _photoService = photoService;
  }

  public async Task<Result> Handle(
      CreatePhotoCommand request,
      CancellationToken  cancellationToken)
  {

    try
    {
      return await _photoService.AddPhotoAsync(request.File, new UserId(request.UserId));
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
