using Application.common.DTO;
using Application.common.Exceptions;
using Application.Common.Interfaces;
using Application.common.Mappings;
using Application.common.Models;

using AutoMapper;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.Queries.GetActivity;

public record GetActivityByIdQuery : IRequest<ActivityDto>
{
  public Guid Id { get; init; }
}

public class
    GetActivityByIdQueryHandler : IRequestHandler<GetActivityByIdQuery, ActivityDto>
{
  private readonly IApplicationDbContext                _context;
  private readonly IMapper                              _mapper;
  private readonly ILogger<GetActivityByIdQueryHandler> _logger;

  public GetActivityByIdQueryHandler(
      IApplicationDbContext                context,
      IMapper                              mapper,
      ILogger<GetActivityByIdQueryHandler> logger)
  {
    _context = context;
    _mapper = mapper;
    _logger = logger;
  }

  public async Task<ActivityDto> Handle(
      GetActivityByIdQuery request,
      CancellationToken    cancellationToken)
  {
    var entity = await _context.Activities.FindAsync(new object[ ] { request.Id },
                                                     cancellationToken);

    if (entity == null)
    {
      _logger.LogError("Could not find activity with id {Id}", request.Id);

      throw new NotFoundException(nameof(Activity), request.Id);
    }

    try
    {
      var activityDto = _mapper.Map<ActivityDto>(entity);

      return activityDto;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error Mapping to DTO: {ExMessage}", ex.Message);

      throw;
    }
  }
}
