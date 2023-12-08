using Application.common.DTO;
using Application.common.Interfaces;
using Application.Common.Interfaces;
using Application.common.Mappings;

using AutoMapper;

using Domain.Entities;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.Queries.GetActivity;

public record GetAllActivitiesQuery : IRequest<List<ActivityDTO>>;

public class
    GetAllActivitiesQueryHandler : IRequestHandler<GetAllActivitiesQuery,
    List<ActivityDTO>>
{
  private readonly IApplicationDbContext                 _context;
  private readonly ILogger<GetAllActivitiesQueryHandler> _logger;
  private readonly IMapper                               _mapper;
  private readonly IUserService                          _userService;

  public GetAllActivitiesQueryHandler(
      IApplicationDbContext                 context,
      IMapper                               mapper,
      ILogger<GetAllActivitiesQueryHandler> logger,
      IMediator                             mediator,
      IUserService                          userService)
  {
    _context = context;
    _mapper = mapper;
    _logger = logger;
    _userService = userService;
  }

  public async Task<List<ActivityDTO>> Handle(
      GetAllActivitiesQuery request,
      CancellationToken     cancellationToken)
  {
    var entity = await
        _context.Activities
                .ProjectToListAsync<ActivityDTO>(_mapper.ConfigurationProvider);

    if (entity.Any()) return entity;

    _logger.LogError("Could not find activities");

    throw new NotFoundException(nameof(Activity));
  }
}
