// PhotoService.cs

using System;
using System.Threading;
using System.Threading.Tasks;

using Application.common.DTO;
using Application.common.Interfaces;
using Application.common.Models;

using Domain.Entities;
using Domain.Events.Photo;
using Domain.Repositories;
using Domain.ValueObjects;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Service;

public class PhotoService : IPhotoService
{
  private readonly IUnitOfWork         _unitOfWork;
  private readonly ICloudinaryService    _cloudinaryService;
  private readonly IPhotoRepository      _photoRepository;
  private readonly ITransactionManager   _transactionManager;
  private readonly ILogger<PhotoService> _logger;

  public PhotoService(
      ICloudinaryService    cloudinaryService,
      IPhotoRepository      photoRepository,
      ITransactionManager   transactionManager,
      ILogger<PhotoService> logger,
      IUnitOfWork           unitOfWork)
  {
    _cloudinaryService = cloudinaryService;
    _photoRepository = photoRepository;
    _transactionManager = transactionManager;
    _logger = logger;
    _unitOfWork = unitOfWork;
  }

  public async Task<Result> AddPhotoAsync(IFormFile file, UserId userId)
  {
    PhotoUploadDTO? uploadResult = null;

    await _transactionManager.BeginTransactionAsync();

    try
    {
      uploadResult = await _cloudinaryService.UpLoadPhotoAsync(file);

      if (uploadResult == null)
      {
        await _transactionManager.RollbackTransactionAsync();

        return Result.Failure(new[ ] { "Photo upload failed" });
      }

      var isMainPhotoExist = await _photoRepository.GetMainPhotoByUserIdAsync(userId,
        CancellationToken.None) != null;

      var photo = Photo.Add(uploadResult.PublicId, uploadResult.Url, !isMainPhotoExist, userId);
      await _photoRepository.AddAsync(photo, new CancellationToken());

      await _transactionManager.CommitTransactionAsync();

      photo.AddDomainEvent(new PhotoAddedDomainEvent(photo));

      return Result.Success();
    }
    catch (Exception ex)
    {
      await _transactionManager.RollbackTransactionAsync();

      // 尝试删除云端照片
      if (uploadResult != null)
      {
        await _cloudinaryService.DeletePhotoAsync(uploadResult.PublicId);
      }

      _logger.LogError(ex, "Error occurred during photo addition");

      return Result.Failure(new[ ] { ex.Message });
    }
  }

  public async Task<Result> RemovePhotoAsync(string publicId, UserId userId)
  {
    await _transactionManager.BeginTransactionAsync();

    try
    {
      // 删除数据库记录
      var photo = await _photoRepository.GetByPublicIdAsync(publicId, CancellationToken.None);

      if (photo != null)
      {
        if (photo.Details.IsMain)
        {
          return Result.Failure(new[ ] { "Cannot delete main photo" });
        }

        _photoRepository.Remove(photo);
        await _transactionManager.CommitTransactionAsync();
        photo.AddDomainEvent(new PhotoRemovedDomainEvent(publicId, userId));
      }
      else
      {
        await _transactionManager.RollbackTransactionAsync();
        return Result.Failure(new[] { "Photo not found in the database" });
      }

      // 删除云端照片
      var deleteResult = await _cloudinaryService.DeletePhotoAsync(publicId);
      if (!deleteResult)
      {
        // 如果云端照片删除失败，则记录错误
        _logger.LogError("Failed to delete the photo with public ID {PublicId} from the cloud service", publicId);
      }

      return Result.Success();
    }
    catch (Exception ex)
    {
      await _transactionManager.RollbackTransactionAsync();
      _logger.LogError(ex, "Error occurred during photo deletion");
      return Result.Failure(new[] { ex.Message });
    }
  }


  public async Task<Result> UpdatePhotoAsync(string publicId, UserId userId)
  {
    var photo = await _photoRepository.GetByPublicIdAsync(publicId, new CancellationToken());

    if (photo is null)
    {
      return Result.Failure(new[ ] { "Photo not found" });
    }

    if (photo.Details.IsMain)
    {
      return Result.Failure(new[ ] { "You cannot update the main photo" });
    }

    var mainPhoto = await _photoRepository.GetMainPhotoByUserIdAsync(userId, new CancellationToken());

    if (mainPhoto == null)
    {
      return Result.Failure(new[ ] { "Main photo not found" });
    }

    photo.Update();
    mainPhoto.Update();

    var result = await _unitOfWork.SaveChangesAsync() > 0;

    return result ? Result.Success() : Result.Failure(new[ ] { "Photo update failed" });
  }
}
