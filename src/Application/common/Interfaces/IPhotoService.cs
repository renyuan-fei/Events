using Application.common.Models;

using Domain.Entities;
using Domain.ValueObjects;

using Microsoft.AspNetCore.Http;

namespace Application.common.Interfaces;

public interface IPhotoService
{
  public Task<Photo?> AddPhotoAsync(IFormFile file, string ownerId);

  public Task<Result> RemovePhotoAsync(string publicId, string ownerId , bool mainProtect = true);

  public Task<Result> UpdatePhotoAsync(string publicId, string ownerId);

  public Task<Result> UpdateMainPhotoAsync(IFormFile file, string ownerId);
}
