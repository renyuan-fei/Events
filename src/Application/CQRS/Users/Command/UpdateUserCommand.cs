using Application.common.DTO;
using Application.common.Interfaces;
using Application.Common.Interfaces;

using AutoMapper;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Users.Command;

public record UpdateUserCommand : IRequest<Unit>
{
  public Guid    UserId { get; init; }
  public UserDTO user   { get; init; }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
{
  private readonly IMapper                           _mapper;
  private readonly ILogger<UpdateUserCommandHandler> _logger;
  private readonly IUserService                      _userService;

  public UpdateUserCommandHandler(
      IMapper                           mapper,
      ILogger<UpdateUserCommandHandler> logger,
      IUserService                      userService)
  {
    _mapper = mapper;
    _logger = logger;
    _userService = userService;
  }

  public async Task<Unit> Handle(
      UpdateUserCommand request,
      CancellationToken cancellationToken)
  {
    try
    {
      var result = _userService.UpdateUserInfoAsync(request.UserId, request.user);

      return result.Result
          ? Unit.Value
          : throw new DbUpdateException("Could not update user.");
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error saving to the database: {ExMessage}", ex.Message);

      throw;
    }
  }
}
