using System.Collections.Concurrent;
using System.Net.WebSockets;

public static class SessionManager
{
    public static ConcurrentDictionary<Guid, Session>  Sessions { get; } = new(); 
}
