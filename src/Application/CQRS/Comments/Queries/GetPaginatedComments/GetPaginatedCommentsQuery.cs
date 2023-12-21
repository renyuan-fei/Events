using Application.common.DTO;
using Application.common.Interfaces;
using Application.Common.Interfaces;
using Application.common.Mappings;
using Application.common.Models;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Comments.Queries.GetPaginatedComments;

public record GetPaginatedCommentsQuery : IRequest<PaginatedList<CommentDTO>>
{
  public PaginatedListParams PaginatedListParams { get; init; }
  public Guid                 ActivityId          { get; init; }
}

public class
    GetPaginatedCommentsQueryHandler : IRequestHandler<GetPaginatedCommentsQuery,
    PaginatedList<CommentDTO>>
{
  private readonly IEventsDbContext                          _context;
  private readonly IMapper                                   _mapper;
  private readonly ILogger<GetPaginatedCommentsQueryHandler> _logger;
  private readonly IUserService                              _userService;

  public GetPaginatedCommentsQueryHandler(
      IEventsDbContext                          context,
      IMapper                                   mapper,
      ILogger<GetPaginatedCommentsQueryHandler> logger,
      IUserService                              userService)
  {
    _context = context;
    _mapper = mapper;
    _logger = logger;
    _userService = userService;
  }

  public async Task<PaginatedList<CommentDTO>> Handle(
      GetPaginatedCommentsQuery request,
      CancellationToken         cancellationToken)
  {
    try
    {
      var comments = _context.Comments.Where(x => x.ActivityId == request.ActivityId);

      if (!comments.Any())
      {
        // throw new NotFoundException(nameof(CommentDTO), request.ActivityId);
        return new PaginatedList<CommentDTO>();
      }


      var result = await comments.OrderBy(comment => comment.LastModified)
                                 .ProjectTo<CommentDTO>(_mapper.ConfigurationProvider)
                                 .PaginatedListAsync(request.PaginatedListParams
                                                         .PageNumber,
                                                     request.PaginatedListParams
                                                         .PageSize);

      var usersId = result.Select(commentDTO => commentDTO.UserId).Distinct().ToList();
      var usersInfo = _userService.GetUsersInfoByIdsAsync(usersId).Result;

      for (var i = 0; i < result.Count; i++)
      {
        result[i].Username = usersInfo[i].UserName;
        result[i].DisplayName = usersInfo[i].DisplayName;
        result[i].Image = usersInfo[i].MainPhoto;
      }

      return result;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error saving to the database: {ExMessage}", ex.Message);

      throw;
    }
  }
}
