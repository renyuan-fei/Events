using Application.common.Interfaces;
using Application.Common.Interfaces;

using AutoMapper;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.Commands.CreateActivity;

/// <summary>
///   Represents the command for creating a new activity.
/// </summary>
public record CreateActivityCommand : IRequest<Unit>
{
  public Guid     CurrentUserId { get; init; }
  public Activity Activity      { get; init; }
}

public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand,
    Unit>
{
  private readonly IApplicationDbContext                 _context;
  private readonly ILogger<CreateActivityCommandHandler> _logger;
  private readonly IMapper                               _mapper;
  private readonly IUserService                          _userService;

  public CreateActivityCommandHandler(
      IApplicationDbContext                 context,
      IMapper                               mapper,
      ILogger<CreateActivityCommandHandler> logger,
      IUserService                          userService)
  {
    _context = context;
    _mapper = mapper;
    _logger = logger;
    _userService = userService;
  }

  public async Task<Unit> Handle(
      CreateActivityCommand request,
      CancellationToken     cancellationToken)
  {
    var activity = new Activity
    {
        Title = request.Activity.Title,
        Description = request.Activity.Description,
        Date = request.Activity.Date,
        Category = request.Activity.Category,
        City = request.Activity.City,
        Venue = request.Activity.Venue,
        Attendees = new List<Domain.Entities.ActivityAttendee>()
    };

    var user = await _userService.GetUserInfoByIdAsync(request.CurrentUserId);

    activity.Attendees.Add(new Domain.Entities.ActivityAttendee
    {
        Id = Guid.NewGuid(),
        IsHost = true,
        UserId = request.CurrentUserId,
        DisplayName = user.DisplayName,
        UserName = user.UserName
    });

    // AddUserAsHostIfValid(request.CurrentUserId, activity);

    _context.Activities.Add(activity);

    try
    {
      var result = await _context.SaveChangesAsync(cancellationToken) > 0;

      return result
          ? Unit.Value
          : throw new DbUpdateException("Could not create activity.");
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error saving to the database: {ExMessage}", ex.Message);

      throw;
    }
  }
}
