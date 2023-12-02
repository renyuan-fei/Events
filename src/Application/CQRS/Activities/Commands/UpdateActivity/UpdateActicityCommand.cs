using Application.common.Exceptions;
using Application.Common.Interfaces;
using Application.common.Models;

using AutoMapper;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.Commands.UpdateActivity;

public record UpdateActivityCommand : IRequest<Unit>
{
  public Guid     Id       { get; init; }
  public Activity Activity { get; init; }
}

public class
    UpdateActivityCommandHandler : IRequestHandler<UpdateActivityCommand, Unit>
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

  public async Task<Unit> Handle(
      UpdateActivityCommand request,
      CancellationToken     cancellationToken)
  {
    var entity =
        await _context.Activities.FindAsync(new object[ ] { request.Id },
                                            cancellationToken);

    if (entity == null)
    {
      _logger.LogError("Could not find activity with id {Id}", request.Id);

      throw new NotFoundException(nameof(Activity), request.Id);
    }

    try
    {
      // 更新实体
      _mapper.Map(request.Activity, entity);

      var result = await _context.SaveChangesAsync(cancellationToken) > 0;

      return result
          ? Unit.Value
          : throw new
              DbUpdateException($"Could not update activity with id ${request.Id}");
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error saving to the database: {ExMessage}", ex.Message);

      throw;
    }
  }
}
