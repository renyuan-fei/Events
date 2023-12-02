using Application.common.Exceptions;
using Application.Common.Interfaces;
using Application.common.Models;

using AutoMapper;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.Commands.CreateActivity;

public record CreateActivityCommand : IRequest<Unit>
{
  public Activity Activity { get; init; }
}

public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand,
    Unit>
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

  public async Task<Unit> Handle(
      CreateActivityCommand request,
      CancellationToken     cancellationToken)
  {
    var activity = new Activity
    {
        Title = request.Activity.Title,
        Description = request.Activity.Description,
        Date = request.Activity.Date,
        Category = request.Activity.Category,
        City = request.Activity.City,
        Venue = request.Activity.Venue
    };

    _context.Activities.Add(activity);

    try
    {
      var result = await _context.SaveChangesAsync(cancellationToken) > 0;

      return result
          ? Unit.Value
          : throw new DbUpdateException("Could not create activity.");
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error saving to the database: {ExMessage}", ex.Message);

      throw;
    }
  }
}
