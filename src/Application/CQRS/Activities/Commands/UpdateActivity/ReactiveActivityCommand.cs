using Application.Common.Helpers;
using Application.common.Interfaces;
using Application.Common.Interfaces;
using Application.common.Models;

using AutoMapper;

using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects.Activity;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.Commands.UpdateActivity;

public record ReactiveActivityCommand : IRequest<Result>
{
  public string Id { get; init; }
}

public class
    ReactiveActivityCommandHandler : IRequestHandler<ReactiveActivityCommand, Result>
{
  private readonly IUnitOfWork                             _unitOfWork;
  private readonly IActivityRepository                     _activityRepository;
  private readonly IMapper                                 _mapper;
  private readonly ILogger<ReactiveActivityCommandHandler> _logger;

  public ReactiveActivityCommandHandler(
      IMapper                                 mapper,
      ILogger<ReactiveActivityCommandHandler> logger,
      IActivityRepository                     activityRepository,
      IUnitOfWork                             unitOfWork)
  {
    _mapper = mapper;
    _logger = logger;
    _activityRepository = activityRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task<Result> Handle(
      ReactiveActivityCommand request,
      CancellationToken       cancellationToken)
  {
    try
    {
      var activityId = new ActivityId(request.Id);

      var activity =
          await _activityRepository.GetByIdAsync(activityId, cancellationToken);

      GuardValidation.AgainstNullOrEmpty(nameof(CancelActivityCommand),
                                         $"Activity with id {request.Id} was not found");

      activity!.Reactive();

      await _unitOfWork.SaveChangesAsync(cancellationToken);

      return Result.Success();
    }
    catch (Exception ex)
    {
      _logger.LogError(ex,
                       "Error occurred in {Name}: {ExMessage}",
                       nameof(ReactiveActivityCommand),
                       ex.Message);

      throw;
    }
  }
}

