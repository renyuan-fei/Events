using Application.Common.Interfaces;

using AutoMapper;

using Domain.Entities;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.Commands.CreateActivity;

public record CreateActivityCommand : IRequest<Activity>
{
  public Activity Activity { get; init; }
}

public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, Activity>
{
  private readonly IApplicationDbContext                 _context;
  private readonly IMapper                               _mapper;
  private readonly ILogger<CreateActivityCommandHandler> _logger;

  public CreateActivityCommandHandler(IApplicationDbContext context, IMapper mapper, ILogger<CreateActivityCommandHandler> logger)
  {
    _context = context;
    _mapper = mapper;
    _logger = logger;
  }

  public async Task<Activity> Handle(
      CreateActivityCommand    request,
      CancellationToken cancellationToken)
  {
    _context.Activities.Add(request.Activity);

    await _context.SaveChangesAsync(cancellationToken);

    return request.Activity;
  }
}
