using System.Collections.Concurrent;

namespace WebAPI.SignalR;

public class PresenceTracker
{
    private static readonly ConcurrentDictionary<string, ConcurrentBag<string>> OnlineUsers = 
        new ConcurrentDictionary<string, ConcurrentBag<string>>();
    
    public Task<bool> UserConnected(string username, string connectionId)
    {
        bool isOnline = false;
        OnlineUsers.AddOrUpdate(username, 
            _ => { isOnline = true; return new ConcurrentBag<string> { connectionId }; }, 
            (_, connections) => { connections.Add(connectionId); return connections; });

        return Task.FromResult(isOnline);
    }
    
    public Task<bool> UserDisconnected(string username, string connectionId)
    {
        bool isOffline = false;
        if (OnlineUsers.TryGetValue(username, out var connections))
        {
            connections.TryTake(out _);
            if (!connections.Any())
            {
                isOffline = OnlineUsers.TryRemove(username, out _);
            }
        }

        return Task.FromResult(isOffline);
    }
    
    public Task<string[]> GetOnlineUsers()
    {
        var onlineUsers = OnlineUsers.Keys.OrderBy(k => k).ToArray();
        return Task.FromResult(onlineUsers);
    }
    
    public static Task<ConcurrentBag<string>> GetConnectionsForUser(string username)
    {
        OnlineUsers.TryGetValue(username, out var connectionIds);
        return Task.FromResult(connectionIds);
    }
}