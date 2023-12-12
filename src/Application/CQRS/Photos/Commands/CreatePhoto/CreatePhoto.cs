using Application.common.Interfaces;
using Application.Common.Interfaces;

using AutoMapper;

using Domain;
using Domain.Entities;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Photos.Commands.CreatePhoto;

public record CreatePhoto : IRequest<Unit>
{
  public Guid      UserId { get; init; }
  public IFormFile File   { get; init; }
}

public class CreatePhotoHandler : IRequestHandler<CreatePhoto, Unit>
{
  private readonly IEventsDbContext            _context;
  private readonly IMapper                     _mapper;
  private readonly ILogger<CreatePhotoHandler> _logger;
  private readonly ICloudinaryService          _cloudinaryService;

  public CreatePhotoHandler(
      IEventsDbContext            context,
      IMapper                     mapper,
      ILogger<CreatePhotoHandler> logger,
      ICloudinaryService          cloudinaryService)
  {
    _context = context;
    _mapper = mapper;
    _logger = logger;
    _cloudinaryService = cloudinaryService;
  }

  public async Task<Unit> Handle(
      CreatePhoto       request,
      CancellationToken cancellationToken)
  {
    try
    {
      var photoUploadResult = await _cloudinaryService.UpLoadPhoto(request.File);

      var photo = new Photo
      {
          Url = photoUploadResult!.Url, PublicId = photoUploadResult.PublicId, UserId = request.UserId
      };

      _context.Photos.Add(photo);

      var result = await _context.SaveChangesAsync(cancellationToken) > 0;

      return result
          ? Unit.Value
          : throw new DbUpdateException("Could not save changes.");
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error saving to the database: {ExMessage}", ex.Message);

      throw;
    }
  }
}
