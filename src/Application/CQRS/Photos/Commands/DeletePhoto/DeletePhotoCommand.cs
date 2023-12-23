using Application.common.Interfaces;
using Application.Common.Interfaces;

using AutoMapper;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Photos.Commands.DeletePhoto;

public record DeletePhotoCommand : IRequest<Unit>
{
  public Guid   UserId   { get; init; }
  public string PublicId { get; init; }
}

public class DeletePhotoHandler : IRequestHandler<DeletePhotoCommand, Unit>
{
  private readonly IEventsDbContext            _context;
  private readonly IMapper                     _mapper;
  private readonly ILogger<DeletePhotoHandler> _logger;
  private readonly ICloudinaryService          _cloudinaryService;

  public DeletePhotoHandler(
      IEventsDbContext            context,
      IMapper                     mapper,
      ILogger<DeletePhotoHandler> logger,
      ICloudinaryService          cloudinaryService)
  {
    _context = context;
    _mapper = mapper;
    _logger = logger;
    _cloudinaryService = cloudinaryService;
  }

  public async Task<Unit> Handle(
      DeletePhotoCommand request,
      CancellationToken  cancellationToken)
  {
    const string DefaultImagePublicId = "gyzjw6xpz9pzl0xg7de4";

    try
    {
      if (request.PublicId == DefaultImagePublicId)
      {
        throw new InvalidOperationException("You cannot delete the default image.");
      }

      var photo =
          await _context.Photos.FirstOrDefaultAsync(p => p.UserId == request.UserId
                                                      && p.PublicId == request.PublicId,
                                                    cancellationToken: cancellationToken);

      if (photo == null) { throw new Exception("Photo not found"); }

      if (photo.IsMain)
      {
        _logger.LogError("Cannot delete main photo");

        throw new InvalidOperationException("Cannot delete main photo");
      }

      var result = await _cloudinaryService.DeletePhoto(photo.PublicId);

      if (!result) { throw new Exception("Could not delete photo from cloudinary"); }

      _context.Photos.Remove(photo);

      await _context.SaveChangesAsync(cancellationToken);

      return Unit.Value;
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
