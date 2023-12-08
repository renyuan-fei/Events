using Application.common.DTO;
using Application.Common.Interfaces;

using AutoMapper;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.Queries.GetActivity;

public record GetActivityByIdQuery : IRequest<ActivityDTO>
{
  public Guid Id { get; init; }
}

public class
    GetActivityByIdQueryHandler : IRequestHandler<GetActivityByIdQuery, ActivityDTO>
{
  private readonly IApplicationDbContext                _context;
  private readonly ILogger<GetActivityByIdQueryHandler> _logger;
  private readonly IMapper                              _mapper;

  public GetActivityByIdQueryHandler(
      IApplicationDbContext                context,
      IMapper                              mapper,
      ILogger<GetActivityByIdQueryHandler> logger)
  {
    _context = context;
    _mapper = mapper;
    _logger = logger;
  }

  public async Task<ActivityDTO> Handle(
      GetActivityByIdQuery request,
      CancellationToken    cancellationToken)
  {
    var temp1 = _context.Activities;

    var entity = await _context.Activities.Include(a => a.Attendees)
                               .SingleOrDefaultAsync(a => a.Id == request.Id,
                                                     cancellationToken);

    if (entity == null)
    {
      _logger.LogError("Could not find activity with id {Id}", request.Id);

      throw new NotFoundException(nameof(Activity), request.Id);
    }

    try
    {
      var activityDto = _mapper.Map<ActivityDTO>(entity);

      return activityDto;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error Mapping to DTO: {ExMessage}", ex.Message);

      throw;
    }
  }
}
