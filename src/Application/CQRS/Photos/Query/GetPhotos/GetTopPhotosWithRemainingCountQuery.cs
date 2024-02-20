using Application.common.DTO;
using Application.Common.Interfaces;

using AutoMapper;

using Domain.Entities;
using Domain.Repositories;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Photos.Query.GetPhotos;

public record
    GetTopPhotosWithRemainingCountQuery : IRequest<TopPhotosWithRemainingCountDto>
{
  public string OwnerId;
}

public class GetTopPhotosWithRemainingCountQueryHandler : IRequestHandler<
    GetTopPhotosWithRemainingCountQuery, TopPhotosWithRemainingCountDto>
{
  private readonly IPhotoRepository                                    _photoRepository;
  private readonly IMapper                                             _mapper;
  private readonly ILogger<GetTopPhotosWithRemainingCountQueryHandler> _logger;

  public GetTopPhotosWithRemainingCountQueryHandler(
      IPhotoRepository                                    photoRepository,
      IMapper                                             mapper,
      ILogger<GetTopPhotosWithRemainingCountQueryHandler> logger)
  {
    _photoRepository = photoRepository;
    _mapper = mapper;
    _logger = logger;
  }

  public async Task<TopPhotosWithRemainingCountDto> Handle(
      GetTopPhotosWithRemainingCountQuery request,
      CancellationToken                   cancellationToken)
  {
    try
    {
      var photos =
          _photoRepository.GetPhotosWithoutMainPhotoByOwnerIdQueryable(request.OwnerId,
            cancellationToken);

      // get top 6 photos excluding the main photo
      var topPhotos = await photos.Where(photo => photo.Details.IsMain != true)
                                  .Take(6)
                                  .ToListAsync(cancellationToken);

      var remainingCount = 0;

      if (photos.Count() > 6) { remainingCount = photos.Count() - topPhotos.Count; }

      var photoDtos = _mapper.Map<List<PhotoDto>>(topPhotos);

      return new TopPhotosWithRemainingCountDto
      {
          Photos = photoDtos, RemainingCount = remainingCount
      };
    }
    catch (Exception ex)
    {
      _logger.LogError(ex,
                       "Error occurred in {Name}: {ExMessage}",
                       nameof(GetTopPhotosWithRemainingCountQuery),
                       ex.Message);

      throw;
    }
  }
}
