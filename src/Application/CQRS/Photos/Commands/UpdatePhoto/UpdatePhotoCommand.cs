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
      throw new NotImplementedException();    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "ErrorMessage saving to the database: {ExMessage}", ex.Message);

      throw;
    }
  }
}
