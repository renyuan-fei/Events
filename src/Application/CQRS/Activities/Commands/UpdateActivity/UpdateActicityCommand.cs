using Application.common.DTO;
using Application.Common.Helpers;
using Application.common.Interfaces;
using Application.common.Models;

using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects.Activity;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.Commands.UpdateActivity;

public record UpdateActivityCommand : IRequest<Result>
{
  public string      Id       { get; init; }
  public ActivityDto Activity { get; init; }
}

public class
    UpdateActivityCommandHandler : IRequestHandler<UpdateActivityCommand, Result>
{
  private readonly IActivityRepository                   _activityRepository;
  private readonly ILogger<UpdateActivityCommandHandler> _logger;
  private readonly IMapper                               _mapper;
  private readonly IUnitOfWork                           _unitOfWork;

  public UpdateActivityCommandHandler(
      IMapper                               mapper,
      ILogger<UpdateActivityCommandHandler> logger,
      IActivityRepository                   activityRepository,
      IUnitOfWork                           unitOfWork)
  {
    _mapper = mapper;
    _logger = logger;
    _activityRepository = activityRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task<Result> Handle(
      UpdateActivityCommand request,
      CancellationToken     cancellationToken)
  {
    try
    {
      var activity =
          await _activityRepository.GetByIdAsync(new ActivityId(request.Id),
                                                 cancellationToken);

      GuardValidation.AgainstNull(activity, "Activity with Id: {Id} not found.", request.Id);

      activity.Update(_mapper.Map<Activity>(request.Activity));

      var result = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

      if (!result)
      {
        throw new DbUpdateException("There was an error saving activity data to the database");
      }

      return Result.Success();
    }
    catch (Exception ex)
    {
      _logger.LogError(ex,
                       "ErrorMessage saving to the database: {ExMessage}",
                       ex.Message);

      throw;
    }
  }
}
