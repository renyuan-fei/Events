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

public record GetAllActivitiesQuery : IRequest<List<ActivityDto>>;

public class
    GetAllActivitiesQueryHandler : IRequestHandler<GetAllActivitiesQuery,
        List<ActivityDto>>
{
  private readonly IApplicationDbContext                 _context;
  private readonly IMapper                               _mapper;
  private readonly ILogger<GetAllActivitiesQueryHandler> _logger;

  public GetAllActivitiesQueryHandler(
      IApplicationDbContext                 context,
      IMapper                               mapper,
      ILogger<GetAllActivitiesQueryHandler> logger)
  {
    _context = context;
    _mapper = mapper;
    _logger = logger;
  }

  public async Task<List<ActivityDto>> Handle(
      GetAllActivitiesQuery request,
      CancellationToken     cancellationToken)
  {
    var entity = await
        _context.Activities
                .ProjectToListAsync<ActivityDto>(_mapper.ConfigurationProvider);

    if (entity.Any()) return entity;

    _logger.LogError("Could not find activities");

    throw new NotFoundException(nameof(Activity));

  }
}
