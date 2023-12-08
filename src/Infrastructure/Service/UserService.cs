using Application.common.DTO;
using Application.common.Interfaces;

using AutoMapper;

using Infrastructure.Identity;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Service;

public class UserService : IUserService
{
  private readonly IMapper                      _mapper;
  private readonly UserManager<ApplicationUser> _userManager;

  public UserService(IMapper mapper, UserManager<ApplicationUser> userManager)
  {
    _mapper = mapper;
    _userManager = userManager;
  }

  public async Task<UserInfoDTO> GetUserInfoByIdAsync(Guid userId)
  {
    var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);

    var result = new UserInfoDTO
    {
        UserName = user!.UserName,
        Email = user.Email,
        DisplayName = user.DisplayName,
        Bio = user.Bio,
        PhoneNumber = user.PhoneNumber
    };

    return result;
  }

  public Task<UserInfoDTO> GetUserInfoByEmailAsync(string email)
  {
    throw new NotImplementedException();
  }

  public Task<UserInfoDTO> GetUserInfoByPhoneNumberAsync(string phoneNumber)
  {
    throw new NotImplementedException();
  }
}
