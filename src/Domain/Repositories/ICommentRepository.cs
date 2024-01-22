using Domain.ValueObjects.Activity;

namespace Domain.Repositories;

public interface ICommentRepository
{
  List<Comment> GetCommentsByActivityId(ActivityId activityId);
}
