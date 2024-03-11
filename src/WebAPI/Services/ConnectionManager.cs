using System.Collections.Concurrent;

using Application.common.Interfaces;

namespace WebAPI.Services;

public class ConnectionManager : IConnectionManager
{
  // Key是用户ID，Value是该用户的连接ID集合
  private readonly ConcurrentDictionary<string, HashSet<string>> _connections = new();

  public void AddConnection(string userId, string connectionId)
  {
    _connections.AddOrUpdate(userId, new HashSet<string> { connectionId },
                             (key, existingHashSet) =>
                             {
                               existingHashSet.Add(connectionId);
                               return existingHashSet;
                             });
  }

  public void RemoveConnection(string connectionId)
  {
    foreach (var userId in _connections.Keys)
    {
      HashSet<string> connections;

      if (!_connections.TryGetValue(userId, out connections)) continue;

      if (!connections.Remove(connectionId)
       || connections.Count != 0)
        continue;

      // 如果用户没有其他连接，从字典中移除该用户
      HashSet<string> removed;
      _connections.TryRemove(userId, out removed);
    }
  }

  public IReadOnlyList<string> GetUserConnections(string userId)
  {
    if (_connections.TryGetValue(userId, out var connections))
    {
      return connections.ToList().AsReadOnly();
    }

    return new List<string>().AsReadOnly();
  }

  public IReadOnlyList<string> GetConnectionsForUsers(List<string> userIds)
  {
    var connectionIds = new List<string>();
    foreach (var userId in userIds)
    {
      if (_connections.TryGetValue(userId, out var userConnections))
      {
        connectionIds.AddRange(userConnections);
      }
    }
    return connectionIds;
  }

}
