using System.Net.WebSockets;
using NLog.LayoutRenderers;

public sealed class ConnectSessionWS : IConnectSession<WebSocket>
{
    private readonly IUserIdentifierProvider _userIdentifierProvider;
    private readonly GameDataStorage _gameStorage;

    public ConnectSessionWS(IUserIdentifierProvider userIdentifierProvider, GameDataStorage gameStorage)
    {
        _userIdentifierProvider = userIdentifierProvider;
        _gameStorage = gameStorage;
    }

    public bool Connect(HttpContext context, WebSocket socket, ITransportSender transport, out Session? session)
    {
        session = null;
        if (!_userIdentifierProvider.TryGetUserId(context, out var userId))
           return false;

        var user = _gameStorage.GetUser(userId);
        if (user == null)
            return false;

        session = new Session(socket, transport);
        session.SetUser(user);

        return SessionManager.Sessions.TryAdd(session.SessionId, session);
    }
}
