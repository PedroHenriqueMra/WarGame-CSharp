using System.Net.WebSockets;

public class Session
{
    public Guid SessionId { get; } = Guid.NewGuid();
    public WebSocket Socket { get; set; }
    public ITransportSender Transport { get; set; }

    public User User { get; set; }

    public Session (WebSocket socket, ITransportSender transport)
    {
        this.Socket = socket;
        this.Transport = transport;

        this.User = User.CreateGuest();
    }

    public void SetUser(User user)
        => this.User = user;

    public Task SendAsync(byte[] bytes)
    {
        return this.Transport.SendAsync(bytes);
    }
}
