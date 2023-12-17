using Application.common.DTO;
using Application.common.Interfaces;

using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

using Infrastructure.security;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure.Service;

public class CloudinaryService : ICloudinaryService
{
  private readonly Cloudinary _cloudinary;

  public CloudinaryService(IOptions<CloudinarySettings> config)
  {
    var account = new Account(config.Value.CloudName,
                              config.Value.ApiKey,
                              config.Value.ApiSecret);

    _cloudinary = new Cloudinary(account);
  }

  public async Task<PhotoUploadDTO?> UpLoadPhoto(IFormFile file)
  {
    if (file.Length <= 0) return null;

    await using var stream = file.OpenReadStream();

    var uploadParams = new ImageUploadParams
    {
        File = new FileDescription(file.FileName, stream),
        Transformation = new Transformation().Height(500).Width(500).Crop("fill")
    };

    var uploadResult = await _cloudinary.UploadAsync(uploadParams);

    if (uploadResult.Error != null) { throw new Exception(uploadResult.Error
        .Message); }

    return new PhotoUploadDTO()
    {
        PublicId = uploadResult.PublicId, Url = uploadResult.SecureUrl.ToString()
    };
  }

  public async Task<bool> DeletePhoto(string publicId)
  {
    var deleteParams = new DeletionParams(publicId);
    var result = await _cloudinary.DestroyAsync(deleteParams);

    return result.Result == "ok";
  }
}
