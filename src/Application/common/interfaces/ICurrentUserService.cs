namespace Application.common.interfaces;

public interface ICurrentUserService
{
  Guid? UserId { get; }
}
