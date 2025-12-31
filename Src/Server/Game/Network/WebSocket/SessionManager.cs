using System.Collections.Concurrent;
using System.Net.WebSockets;

public static class SessionManager
{
    public static ConcurrentDictionary<Guid, Session>  Sessions { get; } = new();

    public static Session? GetSessionByUserId(Guid userId)
    {
        var session = Sessions.Values.FirstOrDefault(s => s.User.UserId == userId);
        return session;
    }
}
