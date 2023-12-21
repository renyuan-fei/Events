using Application.common.DTO;
using Application.common.Interfaces;
using Application.Common.Interfaces;
using Application.common.Mappings;

using AutoMapper;

using Domain.Entities;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.CQRS.Comments.Queries.GetComments;

public record GetCommentsQuery : IRequest<List<CommentDTO>>
{
  public Guid ActivityId { get; init; }
}

public class GetCommentsQueryHandler : IRequestHandler<GetCommentsQuery, List<CommentDTO>>
{
  private readonly IEventsDbContext                 _context;
  private readonly IMapper                          _mapper;
  private readonly ILogger<GetCommentsQueryHandler> _logger;
  private readonly IUserService                     _userService;

  public GetCommentsQueryHandler(
      IEventsDbContext                 context,
      IMapper                          mapper,
      ILogger<GetCommentsQueryHandler> logger,
      IUserService                     userService)
  {
    _context = context;
    _mapper = mapper;
    _logger = logger;
    _userService = userService;
  }

  public async Task<List<CommentDTO>> Handle(
      GetCommentsQuery  request,
      CancellationToken cancellationToken)
  {
    try
    {
      var comments = _context.Comments.Where(x => x.ActivityId == request.ActivityId);

      if (!comments.Any())
      {
        // throw new NotFoundException(nameof(CommentDTO), request.ActivityId);
        return new List<CommentDTO>();
      }

      var result = await comments.OrderBy(comment => comment.LastModified)
                                 .ProjectToListAsync<
                                     CommentDTO>(_mapper.ConfigurationProvider);

      var usersId = result.Select(commentDTO => commentDTO.UserId).Distinct().ToList();
      var usersInfo = _userService.GetUsersInfoByIdsAsync(usersId).Result;

      foreach (var t in result)
      {
        var user = usersInfo.FirstOrDefault(u => u.Id == t.UserId);

        if (user == null) { throw new NotFoundException(nameof(UserDTO), t.UserId); }

        t.Username = user.UserName;
        t.DisplayName = user.DisplayName;
        t.Image = user.MainPhoto;
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
