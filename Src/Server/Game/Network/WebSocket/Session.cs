using System.Net.WebSockets;

public struct Session
{
    public Guid SessionId { get; } = Guid.NewGuid();
    public WebSocket WebSocket { get; set; }

    public User User { get; set; }

    public Session (WebSocket ws)
    {
        this.WebSocket = ws;

        this.User = User.CreateGuest();
    }

    public void Authenticate(Guid userId, string username)
    {
        this.User = User.CreateAuthenticated(userId, username);
    }

    public Task SendAsync(byte[] bytes)
    {
        return this.WebSocket.SendAsync(bytes, WebSocketMessageType.Text, true, CancellationToken.None);
    }
}
