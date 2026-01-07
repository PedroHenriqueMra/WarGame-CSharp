using System.Net.WebSockets;
using System.Text;

public class WebSocketHandler
{
    private static NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

    private readonly InputAdmin _inputAdmin;
    public WebSocketHandler(InputAdmin inputAdmin)
    {
        this._inputAdmin = inputAdmin;
    }

    public async Task HandleWebSocketAsync(WebSocket socket)
    {
        var session = new Session(socket);
        SessionManager.Sessions.TryAdd(session.SessionId, session);
        Logger.Info($"New session created: {session.SessionId}. Guest: Id: {session.User.UserId}, Name: {session.User.Username};");

        var buffer = new byte[1024 * 4]; // 4kb

        try
        {
            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Close)
                    break;

                var json = Encoding.UTF8.GetString(buffer, 0, result.Count);
                Logger.Info($"Json received: {json}");

                _inputAdmin.Handle(json, session);
            }
        }
        finally
        {
            Logger.Info($"Session closed: {session.SessionId}");
            SessionManager.Sessions.TryRemove(session.SessionId, out _);
        }

        await socket.CloseAsync(
            WebSocketCloseStatus.NormalClosure,
            "Closing",
            CancellationToken.None
        );
    }
}
