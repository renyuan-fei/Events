using Application.common.DTO;
using Application.common.Interfaces;
using Application.Common.Interfaces;

using AutoMapper;

using Domain.Entities;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Users.Queries.GetUser;

// Used for bypassing authorization behavior checks.
[BypassAuthorization]
public record GetUserQuery : IRequest<UserInfoDTO>
{
  public Guid UserId { get; init; }
}

public class GetUserHandler : IRequestHandler<GetUserQuery, UserInfoDTO>
{
  private readonly IMapper                 _mapper;
  private readonly ILogger<GetUserHandler> _logger;
  private readonly IUserService            _userService;

  public GetUserHandler(
      IMapper                 mapper,
      ILogger<GetUserHandler> logger,
      IUserService            userService)
  {
    _mapper = mapper;
    _logger = logger;
    _userService = userService;
  }

  public async Task<UserInfoDTO> Handle(
      GetUserQuery           request,
      CancellationToken cancellationToken)
  {
    try
    {
      throw new NotImplementedException();
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "ErrorMessage saving to the database: {ExMessage}", ex.Message);

      throw;
    }
  }
}
