using Application.common.DTO;
using Application.common.Interfaces;
using Application.Common.Interfaces;
using Application.common.Models;

using AutoMapper;

using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects.Activity;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.Commands.UpdateActivity;

public record UpdateActivityCommand : IRequest<Result>
{
  public string      Id       { get; init; }
  public ActivityDTO Activity { get; init; }
}

public class
    UpdateActivityCommandHandler : IRequestHandler<UpdateActivityCommand, Result>
{
  private readonly IUnitOfWork                           _unitOfWork;
  private readonly IActivityRepository                   _activityRepository;
  private readonly ILogger<UpdateActivityCommandHandler> _logger;
  private readonly IMapper                               _mapper;

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

      Guard.Against.Null(activity, "Activity with Id: {Id} not found.", request.Id);

      activity.Update(_mapper.Map<Activity>(request.Activity));

      var result = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

      return result
          ? Result.Success()
          : Result.Failure(new []{"Could not update activity."});
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
