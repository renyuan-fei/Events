using Application.common.DTO;
using Application.common.Exceptions;
using Application.common.Interfaces;
using Application.Common.Interfaces;
using Application.common.Mappings;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Infrastructure.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Service;

public class UserService : IUserService
{
  private readonly IMapper                      _mapper;
  private readonly UserManager<ApplicationUser> _userManager;
  private readonly ILogger<UserService>         _logger;
  private readonly IEventsDbContext             _context;

  public UserService(
      IMapper                      mapper,
      UserManager<ApplicationUser> userManager,
      ILogger<UserService>         logger,
      IEventsDbContext             context)
  {
    _mapper = mapper;
    _userManager = userManager;
    _logger = logger;
    _context = context;
  }

  public async Task<UserInfoDTO> GetUserInfoByIdAsync(
      Guid userId,
      bool   includePhotos = false)
  {

    var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);

    if (user == null)
    {
      _logger.LogError("User not found");

      throw new NotFoundException($"User {userId} not found.");
    }

    var photos = new List<PhotoDTO?>();

    if (includePhotos)
    {
      photos = (await _context.Photos.Where(p => p.UserId == userId)
                              .ProjectToListAsync<
                                  PhotoDTO>(_mapper.ConfigurationProvider))!;
    }

    var mainPhoto =
        _context.Photos.FirstOrDefault(p => p.UserId == userId && p.IsMain == true)?.Url;

    var result = new UserInfoDTO
    {
        Id = user.Id,
        UserName = user.UserName,
        Email = user.Email,
        DisplayName = user.DisplayName,
        Bio = user.Bio,
        PhoneNumber = user.PhoneNumber,
        MainPhoto = mainPhoto,
        Photos = photos
    };

    return result;
  }

  public async Task<List<UserInfoDTO>> GetUsersInfoByIdsAsync(List<Guid> userIds)
  {
    var users = await _userManager.Users
                                  .Where(u => userIds.Contains(u.Id))
                                  .ToListAsync();

    if (users == null)
    {
      _logger.LogError("Users not found");

      throw new NotFoundException("Users not found.");
    }

    return users.Select(user => new UserInfoDTO
                {
                    Id = user.Id,
                    UserName = user!.UserName,
                    Email = user.Email,
                    DisplayName = user.DisplayName,
                    Bio = user.Bio,
                    PhoneNumber = user.PhoneNumber,
                    MainPhoto = _context.Photos.FirstOrDefault(p => p.UserId ==
                        user.Id && p.IsMain == true)?.Url!
                })
                .ToList();
  }

  public async Task<UserInfoDTO> GetUserInfoByEmailAsync(
      string email,
      bool   includePhotos = false)
  {
    throw new NotImplementedException();
  }

  public async Task<UserInfoDTO> GetUserInfoByPhoneNumberAsync(
      string phoneNumber,
      bool   includePhotos = false)
  {
    throw new NotImplementedException();
  }

  public async Task<bool> UpdateUserInfoAsync(Guid userId, UserInfoDTO userInfoDTO)
  {
    var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);

    if (user == null) { return false; }

    // Use the AutoMapper to update the user details
    _mapper.Map(userInfoDTO, user);

    // Store the updated user back into the persistence layer
    var result = await _userManager.UpdateAsync(user);

    // If the update was successful, the result.Succeeded would be true.
    return result.Succeeded;
  }

  public async Task<bool> DeleteUserAsync(Guid userId)
  {
    var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);

    if (user == null)
    {
      return false; // User not found, so return false
    }

    var result = await _userManager.DeleteAsync(user);

    // If the delete operation was successful,
    // the result.Succeeded would be true.
    return result.Succeeded;
  }

  public async Task<bool> SetMainPhotoAsync(Guid userId, Guid photoId)
  {
    throw new NotImplementedException();
  }
}
