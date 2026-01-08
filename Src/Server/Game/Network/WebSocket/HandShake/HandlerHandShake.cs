using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

public sealed class HandlerHandShake
{
    private static NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

    private readonly IUserIdentifierProvider _userIdentifierProvider;
    private readonly IRoomBindingAccess _roomBindingAccess;
    public HandlerHandShake(IUserIdentifierProvider userIdentifierProvider, IRoomBindingAccess roomBindingAccess)
    {
        _userIdentifierProvider = userIdentifierProvider;
        _roomBindingAccess = roomBindingAccess;
    }

    public async Task<HandShakeResult> HandleAsync(WebSocket socket, HttpContext context)
    {
        if (!_userIdentifierProvider.TryGetUserId(context, out Guid userId))
        {
            Logger.Info("Invalid token");
            return HandShakeResult.Failed("Invalid token");
        }

        // Wait user sends token
        var input = await ReceiveTokenAsync(socket);
        if (input.Status == StatusHandShakeResult.failed)
        {
            Logger.Info(input.Message);
            return input;
        }

        var content = (HandShakeEnvelope<RoomIdPayLoad>)input.Content!;

        if (!_roomBindingAccess.IsMember(userId, content.Payload.RoomId))
        {
            Logger.Info($"User {userId} is not a member of room {content.Payload.RoomId}");
            return HandShakeResult.Failed($"User {userId} is not a member of room {content.Payload.RoomId}");
        }

        return HandShakeResult.Success(content.Payload);
    }

    private async Task<HandShakeResult> ReceiveTokenAsync(WebSocket socket)
    {
        try
        {
            var buffer = new byte[1024 * 4];

            var result = await socket.ReceiveAsync(buffer, CancellationToken.None);
            var json = Encoding.UTF8.GetString(buffer, 0, result.Count);

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var envelope = JsonSerializer.Deserialize<HandShakeEnvelope<RoomIdPayLoad>>(json, options);

            if (envelope == null)
                return HandShakeResult.Failed("Invalid hand shake");

            return HandShakeResult.Success(envelope.Payload);
        }
        catch (Exception ex)
        {
            var message = new StringBuilder($"An error occurred. Ex Message: {ex.Message}");
            Logger.Error(message);
            return HandShakeResult.Failed(message.ToString());
        }
    }
}
