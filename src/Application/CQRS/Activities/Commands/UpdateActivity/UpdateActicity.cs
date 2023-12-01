using Application.Common.Interfaces;
using Application.common.Models;

using AutoMapper;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.Commands.UpdateActivity;

public record UpdateActivityCommand : IRequest<Result>
{
  public Guid     Id       { get; init; }
  public Activity Activity { get; init; }
}

public class
    UpdateActivityCommandHandler : IRequestHandler<UpdateActivityCommand, Result>
{
  private readonly IApplicationDbContext                 _context;
  private readonly IMapper                               _mapper;
  private readonly ILogger<UpdateActivityCommandHandler> _logger;

  public UpdateActivityCommandHandler(
      IApplicationDbContext                 context,
      IMapper                               mapper,
      ILogger<UpdateActivityCommandHandler> logger)
  {
    _context = context;
    _mapper = mapper;
    _logger = logger;
  }

  public async Task<Result> Handle(
      UpdateActivityCommand request,
      CancellationToken     cancellationToken)
  {
    var entity =
        await _context.Activities.FindAsync(new object[ ] { request.Id },
                                            cancellationToken);

    if (entity == null) return null!;

    // 更新Activity
    _mapper.Map(request.Activity, entity);

    var result = await _context.SaveChangesAsync(cancellationToken) > 0;

    return result
        ? Result.Success()
        : Result.Failure(new[ ] { "Failed to update Activity" });
  }
}
