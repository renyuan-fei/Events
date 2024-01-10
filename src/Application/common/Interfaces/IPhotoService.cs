using Application.common.Models;

using Domain.ValueObjects;

using Microsoft.AspNetCore.Http;

namespace Application.common.Interfaces;

public interface IPhotoService
{
  public Task<Result> AddPhotoAsync(IFormFile file, string ownerId);

  public Task<Result> RemovePhotoAsync(string publicId, string ownerId);

  public Task<Result> UpdatePhotoAsync(string publicId, string ownerId);
}
