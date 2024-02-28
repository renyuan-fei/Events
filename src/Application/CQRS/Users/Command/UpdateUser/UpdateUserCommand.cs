using Application.common.DTO;
using Application.Common.Helpers;
using Application.common.Interfaces;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Users.Command.UpdateUser;

public record UpdateUserCommand : IRequest<Unit>
{
    public string UserId { get; init; }
    public UserDto User { get; init; }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
{
    private readonly IUserService _userService;
    private readonly ILogger<UpdateUserCommandHandler> _logger;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(
        IUserService userService,
        ILogger<UpdateUserCommandHandler> logger,
        IMapper mapper)
    {
        _userService = userService;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(
        UpdateUserCommand request,
        CancellationToken cancellationToken)
    {
        var requestUserId = request.UserId;
        var requestUser = request.User;

        try
        {
            // Update the user with the new values
            await _userService.UpdateUserAsync(requestUser);

            _logger.LogInformation("User with ID {UserId} updated successfully", requestUserId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating user with ID {UserId}", requestUserId);
            throw;
        }

        return Unit.Value;
    }
}
