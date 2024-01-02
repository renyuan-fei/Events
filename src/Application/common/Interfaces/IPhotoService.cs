using Application.common.Models;

using Domain.Entities;
using Domain.ValueObjects;

using Microsoft.AspNetCore.Http;

namespace Application.common.Interfaces;

public interface IPhotoService
{
  public Task<Result> AddPhotoAsync(IFormFile file, UserId userId);

  public Task<Result> RemovePhotoAsync(string publicId, UserId userId);

  public Task<Result> UpdatePhotoAsync(string publicId, UserId userId);
}
