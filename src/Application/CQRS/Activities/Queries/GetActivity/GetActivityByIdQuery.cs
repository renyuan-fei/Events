using Application.Common.Interfaces;

using AutoMapper;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.CQRS.Activities.Queries.GetActivity;

public record GetActivityByIdQuery : IRequest<Activity>
{
  public Guid Id { get; init; }
}

public class
    GetActivityByIdQueryHandler : IRequestHandler<GetActivityByIdQuery, Activity>
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

  public async Task<Activity> Handle(
      GetActivityByIdQuery request,
      CancellationToken    cancellationToken)
  {
    return (await _context.Activities.FindAsync(new object[] { request.Id }, cancellationToken))!;
  }
}
