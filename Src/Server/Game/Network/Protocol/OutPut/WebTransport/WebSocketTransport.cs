using System.Net.WebSockets;

public class WebSocketTransport : ITransportSender
{
    private readonly WebSocket _socket;

    public WebSocketTransport(WebSocket socket)
    {
        _socket = socket;
    }

    public async Task SendAsync(byte[] bytes)
    {
        _socket.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
    }    
}
