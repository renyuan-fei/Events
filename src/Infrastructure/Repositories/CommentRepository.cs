using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects.Activity;
using Domain.ValueObjects.Comment;

using Infrastructure.DatabaseContext;

namespace Infrastructure.Repositories;

public class CommentRepository : Repository<Comment, CommentId>, ICommentRepository
{
  public CommentRepository(EventsDbContext dbContext) : base(dbContext) { }

  public List<Comment> GetCommentsByActivityId(ActivityId activityId)
  {
    return DbContext.Comments.Where(comment => comment.ActivityId == activityId)
                    .OrderByDescending(comment => comment.Created)
                    .ToList();
  }
}
