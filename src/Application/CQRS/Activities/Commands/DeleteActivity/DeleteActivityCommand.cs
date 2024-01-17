using Application.Common.Helpers;
using Application.common.Interfaces;
using Application.common.Models;

using Domain.Repositories;
using Domain.ValueObjects.Activity;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.Commands.DeleteActivity;

public record DeleteActivityCommand : IRequest<Result>
{
  public string Id { get; init; }
}

public class
    DeleteActivityCommandHandler : IRequestHandler<DeleteActivityCommand, Result>
{
  private readonly IActivityRepository                   _activityRepository;
  private readonly ILogger<DeleteActivityCommandHandler> _logger;
  private readonly IMapper                               _mapper;
  private readonly IUnitOfWork                           _unitOfWork;

  public DeleteActivityCommandHandler(
      IMapper                               mapper,
      ILogger<DeleteActivityCommandHandler> logger,
      IActivityRepository                   activityRepository,
      IUnitOfWork                           unitOfWork)
  {
    _mapper = mapper;
    _logger = logger;
    _activityRepository = activityRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task<Result> Handle(
      DeleteActivityCommand request,
      CancellationToken     cancellationToken)
  {
    try
    {
      var activity =
          await _activityRepository.GetByIdAsync(new ActivityId(request.Id),
                                                 cancellationToken);

      GuardValidation.AgainstNull(activity, nameof(activity));

      _activityRepository.Remove(activity);

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
