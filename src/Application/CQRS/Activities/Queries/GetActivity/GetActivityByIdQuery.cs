using Application.Common.Interfaces;
using Application.common.Mappings;

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

  public GetActivityByIdQueryHandler(IApplicationDbContext context, IMapper mapper, ILogger<GetActivityByIdQueryHandler> logger)
  {
    _context = context;
    _mapper = mapper;
    _logger = logger;
  }

  public async Task<ActivityDto> Handle(
      GetActivityByIdQuery request,
      CancellationToken    cancellationToken)
  {
    var result = await _context.Activities.FindAsync(new object[] { request.Id },
      cancellationToken)!;

    var activityDto = _mapper.Map<ActivityDto>(result);

    return activityDto;
  }
}
