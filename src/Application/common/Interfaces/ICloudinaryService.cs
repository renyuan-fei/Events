using Application.common.DTO;

using Microsoft.AspNetCore.Http;

namespace Application.common.Interfaces;

public interface ICloudinaryService
{
  Task<PhotoUploadDTO?> UpLoadPhoto(IFormFile file);

  Task<bool> DeletePhoto(string publicId);
}
