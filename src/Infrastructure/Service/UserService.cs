using Application.common.DTO;
using Application.common.Interfaces;

using AutoMapper;

using Infrastructure.Identity;

using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Service;

public class UserService : IUserService
{
  private readonly IMapper _mapper;
  private readonly UserManager<ApplicationUser> _userManager;

  public UserService(IMapper mapper, UserManager<ApplicationUser> userManager)
  {
    _mapper = mapper;
    _userManager = userManager;
  }

  public UserInfoDTO GetUserInfoById(Guid userId)
  {
    var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

    var result = new UserInfoDTO
    {
        UserName = user.UserName,
        Email = user.Email,
        DisplayName = user.DisplayName,
        Bio = user.Bio,
        PhoneNumber = user.PhoneNumber
    };

    return result;
  }

  public UserInfoDTO GetUserInfoByEmail(string email) { throw new NotImplementedException(); }

  public UserInfoDTO GetUserInfoByPhoneNumber(string phoneNumber) { throw new NotImplementedException(); }
}
