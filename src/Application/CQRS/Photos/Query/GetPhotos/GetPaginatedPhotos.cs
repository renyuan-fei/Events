using Application.common.DTO;
using Application.Common.Interfaces;
using Application.common.Mappings;
using Application.common.Models;

using AutoMapper;

using Domain.Entities;
using Domain.Repositories;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Photos.Query.GetPhotos;

public record GetPaginatedPhotos : IRequest<PaginatedList<PhotoDto>>
{
  public PaginatedListParams paginatedListParams { get; init; }
  public string              OwnerId             { get; init; }
}

public class GetPaginatedPhotosHandler : IRequestHandler<GetPaginatedPhotos,
    PaginatedList<PhotoDto>>
{
  private readonly IPhotoRepository                   _photoRepository;
  private readonly IMapper                            _mapper;
  private readonly ILogger<GetPaginatedPhotosHandler> _logger;

  public GetPaginatedPhotosHandler(
      IMapper                            mapper,
      ILogger<GetPaginatedPhotosHandler> logger,
      IPhotoRepository                   photoRepository)
  {
    _mapper = mapper;
    _logger = logger;
    _photoRepository = photoRepository;
  }

  public async Task<PaginatedList<PhotoDto>> Handle(
      GetPaginatedPhotos request,
      CancellationToken  cancellationToken)
  {
    try
    {
      var ownerId = request.OwnerId;
      var pageNumber = request.paginatedListParams.PageNumber;
      var pageSize = request.paginatedListParams.PageSize;
      var initialTimestamp = request.paginatedListParams.InitialTimestamp;

      var photos =
          _photoRepository.GetPhotosWithoutMainPhotoByOwnerIdQueryable(ownerId,
            cancellationToken);

      photos = photos.Where(x => x.Created < initialTimestamp);

      photos = photos.OrderByDescending(x => x.Created);

      var paginatedList = await photos.ProjectTo<PhotoDto>(_mapper.ConfigurationProvider)
                                .PaginatedListAsync(pageNumber,pageSize);
      return paginatedList;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex,
                       "Error occurred in {Name}: {ExMessage}",
                       nameof(GetPaginatedPhotos),
                       ex.Message);

      throw;
    }
  }
}
