namespace Application.common.Interfaces;

public interface IConnectionManager
{
  void                  AddConnection(string      userId, string connectionId);
  void                  RemoveConnection(string   connectionId);
  IReadOnlyList<string> GetUserConnections(string userId);

  IReadOnlyList<string> GetConnectionsForUsers(List<string> userIds);
}
