using Application.Common.Interfaces;

using AutoMapper;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.Commands.UpdateActivity;

public record UpdateActivityCommand : IRequest<Activity>
{
  public Guid     Id       { get; init; }
  public Activity Activity { get; init; }
}

public class
    UpdateActivityCommandHandler : IRequestHandler<UpdateActivityCommand, Activity>
{
  private readonly IApplicationDbContext                 _context;
  private readonly IMapper                               _mapper;
  private readonly ILogger<UpdateActivityCommandHandler> _logger;

  public UpdateActivityCommandHandler(IApplicationDbContext context, IMapper mapper, ILogger<UpdateActivityCommandHandler> logger)
  {
    _context = context;
    _mapper = mapper;
    _logger = logger;
  }

  public async Task<Activity> Handle(
      UpdateActivityCommand request,
      CancellationToken     cancellationToken)
  {
    var entity =
        await _context.Activities.FindAsync(new object[] { request.Id }, cancellationToken);

    if (entity == null) return null!;

    // 更新Activity
    _mapper.Map(request.Activity, entity);

    await _context.SaveChangesAsync(cancellationToken);

    return entity;
  }
}
