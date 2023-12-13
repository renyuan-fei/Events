using Application.Common.Interfaces;

using AutoMapper;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Photos.Commands.UpdatePhoto;

public record UpdatePhotoCommand : IRequest<Unit>
{
  public Guid   UserId   { get; init; }
  public string PublicId { get; init; }
}

public class UpdatePhotoHandler : IRequestHandler<UpdatePhotoCommand, Unit>
{
  private readonly IEventsDbContext            _context;
  private readonly IMapper                     _mapper;
  private readonly ILogger<UpdatePhotoHandler> _logger;

  public UpdatePhotoHandler(
      IEventsDbContext            context,
      IMapper                     mapper,
      ILogger<UpdatePhotoHandler> logger)
  {
    _context = context;
    _mapper = mapper;
    _logger = logger;
  }

  public async Task<Unit> Handle(
      UpdatePhotoCommand       request,
      CancellationToken cancellationToken)
  {
    try
    {
      var photo =
          await _context.Photos.FirstOrDefaultAsync(x => x.UserId == request.UserId
                                                      && x.PublicId == request.PublicId,
                                                    cancellationToken: cancellationToken);

      if (photo == null)
      {
        _logger.LogError("Photo not found");

        throw new NotFoundException("Photo not found");
      }

      if (photo.IsMain) { return Unit.Value; }

      photo.IsMain = true;

      var originalMainPhoto =
          await _context.Photos.FirstOrDefaultAsync(x => x.UserId == photo.UserId
                                                      && x.IsMain,
                                                    cancellationToken: cancellationToken);

      originalMainPhoto!.IsMain = false;

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
