using Domain.Entities;
using Domain.ValueObjects.Comment;

using Infrastructure.DatabaseContext;

namespace Infrastructure.Repositories;

public class CommentRepository : Repository<Comment, CommentId>, ICommentRepository
{
  public CommentRepository(EventsDbContext dbContext) : base(dbContext) { }
}
