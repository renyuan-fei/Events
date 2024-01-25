using Application.Common.Helpers;
using Application.common.Interfaces;
using Application.Common.Interfaces;
using Application.common.Models;

using AutoMapper;

using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects.Activity;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Photos.Commands.CreatePhoto;

public record UploadActivityPhotoCommand : IRequest<Result>
{
  public string    ActivityId { get; init; }
  public IFormFile File       { get; init; }
}

public class
    UploadActivityPhotoCommandHandler : IRequestHandler<UploadActivityPhotoCommand,
    Result>
{
  private readonly IActivityRepository                        _activityRepository;
  private readonly IPhotoRepository                           _photoRepository;
  private readonly IPhotoService                              _photoService;
  private readonly ILogger<UploadActivityPhotoCommandHandler> _logger;

  public UploadActivityPhotoCommandHandler(
      ILogger<UploadActivityPhotoCommandHandler> logger,
      IPhotoService                              photoService,
      IPhotoRepository                           photoRepository,
      IActivityRepository                        activityRepository)
  {
    _logger = logger;
    _photoService = photoService;
    _photoRepository = photoRepository;
    _activityRepository = activityRepository;
  }

  public async Task<Result> Handle(
      UploadActivityPhotoCommand request,
      CancellationToken          cancellationToken)
  {
    try
    {
      var result = await _photoService.AddPhotoAsync(request.File, request.ActivityId);

      return result;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex,
                       "Error occurred in {Name}: {ExMessage}",
                       nameof(UploadActivityPhotoCommand),
                       ex.Message);

      throw;
    }
  }
}
