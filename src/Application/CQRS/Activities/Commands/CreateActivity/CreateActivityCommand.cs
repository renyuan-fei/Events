using Application.Common.Interfaces;
using Application.common.Models;

using AutoMapper;

using Domain.Entities;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.Commands.CreateActivity;

public record CreateActivityCommand : IRequest<Result>
{
  public Activity Activity { get; init; }
}

public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, Result>
{
  private readonly IApplicationDbContext                 _context;
  private readonly IMapper                               _mapper;
  private readonly ILogger<CreateActivityCommandHandler> _logger;

  public CreateActivityCommandHandler(
      IApplicationDbContext                 context,
      IMapper                               mapper,
      ILogger<CreateActivityCommandHandler> logger)
  {
    _context = context;
    _mapper = mapper;
    _logger = logger;
  }

  public async Task<Result> Handle(
      CreateActivityCommand request,
      CancellationToken     cancellationToken)
  {
    _context.Activities.Add(request.Activity);

    var result = await _context.SaveChangesAsync(cancellationToken) > 0;

    return !result
        ? Result.Failure(new[ ] { "Could not create Activity" })
        : Result.Success();
  }
}
