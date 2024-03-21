using Application.common.DTO;
using Application.common.Interfaces;
using Application.common.Models;

using Domain.ValueObjects;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Photos.Commands.CreatePhoto;

public record UploadUserPhotoCommand : IRequest<PhotoDto>
{
  public string    UserId { get; init; }
  public IFormFile File   { get; init; }
}

public class CreatePhotoHandler : IRequestHandler<UploadUserPhotoCommand, PhotoDto>
{
  private readonly IMapper                     _mapper;
  private readonly IUserService                _userService;
  private readonly ILogger<CreatePhotoHandler> _logger;
  private readonly IPhotoService               _photoService;

  public CreatePhotoHandler(
      ILogger<CreatePhotoHandler> logger,
      IPhotoService               photoService,
      IUserService                userService,
      IMapper                     mapper)
  {
    _logger = logger;
    _photoService = photoService;
    _userService = userService;
    _mapper = mapper;
  }

  public async Task<PhotoDto> Handle(
      UploadUserPhotoCommand request,
      CancellationToken  cancellationToken)
  {
    try
    {
      // var isUserExisting = await _userService.IsUserExistingAsync(request.UserId);
      //
      // if (!isUserExisting)
      // {
      //   throw new NotFoundException(nameof(UploadUserPhotoCommand),
      //                               $"User with Id {request.UserId} not found");
      // }

      var photo = await _photoService.AddPhotoAsync(request.File, request.UserId);

      var photoDto = _mapper.Map<PhotoDto>(photo);

      return photoDto;
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
