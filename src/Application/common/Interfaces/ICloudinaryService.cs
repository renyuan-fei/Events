using Application.common.DTO;

using Microsoft.AspNetCore.Http;

namespace Application.common.Interfaces;

public interface ICloudinaryService
{
  Task<PhotoUploadDto?> UpLoadPhotoAsync(IFormFile file);

  Task<bool> DeletePhotoAsync(string publicId);
}
