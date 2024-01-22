using Application.common.DTO;
using Application.common.Interfaces;
using Application.common.Models;

using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using Domain.ValueObjects;
using Domain.ValueObjects.Activity;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.Commands.CreateActivity;

/// <summary>
///   Represents the command for creating a new activity.
/// </summary>
public record CreateActivityCommand : IRequest<Result>
{
  public string      CurrentUserId { get; init; }
  public ActivityDTO ActivityDTO   { get; init; }
}

public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand,
    Result>
{
  private readonly IActivityRepository                   _activityRepository;
  private readonly ILogger<CreateActivityCommandHandler> _logger;
  private readonly IMapper                               _mapper;
  private readonly IUnitOfWork                           _unitOfWork;

  public CreateActivityCommandHandler(
      IMapper                               mapper,
      ILogger<CreateActivityCommandHandler> logger,
      IActivityRepository                   activityRepository,
      IUnitOfWork                           unitOfWork)
  {
    _mapper = mapper;
    _logger = logger;
    _activityRepository = activityRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task<Result> Handle(
      CreateActivityCommand request,
      CancellationToken     cancellationToken)
  {
    try
    {
      var activity = Activity.Create(request.ActivityDTO.Title,
                                     request.ActivityDTO.Date,
                                     Enum.Parse<Category>(request.ActivityDTO.Category),
                                     request.ActivityDTO.Description,
                                     Address.From(request.ActivityDTO.City,
                                                  request.ActivityDTO.Venue));

      var attendee = Attendee.Create(new UserId(request.CurrentUserId),
                                     true,
                                     activity
                                         .Id,
                                     activity);

      activity.AddAttendee(attendee);

      await _activityRepository.AddAsync(activity, cancellationToken);

      var result = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

      if (result) return Result.Success();

      throw new DbUpdateException("There was an error saving data to the database");
    }
    catch (Exception ex)
    {
      _logger.LogError(ex,
                       "Error occurred in {Name}: {ExMessage}",
                       nameof(CreateActivityCommand),
                       ex
                           .Message);

      throw;
    }
  }
}
