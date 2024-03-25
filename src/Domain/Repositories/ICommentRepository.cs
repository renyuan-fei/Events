using Domain.ValueObjects.Activity;

namespace Domain.Repositories;

public interface ICommentRepository
{
  IQueryable<Comment> GetCommentsByActivityId(ActivityId activityId);
}
