using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

public sealed class HandlerHandShake
{
    private static NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

    private readonly IUserIdentifierProvider _userIdentifierProvider;
    private readonly IRoomBindingValidator _roomBindingAccess;

    private readonly SendOutput _sendOutput;
    public HandlerHandShake(IUserIdentifierProvider userIdentifierProvider, IRoomBindingValidator roomBindingAccess, SendOutput sendOutput)
    {
        _userIdentifierProvider = userIdentifierProvider;
        _roomBindingAccess = roomBindingAccess;
        _sendOutput = sendOutput;
    }

    public async Task<HandShakeResult> HandleAsync(WebSocket socket, ITransportSender transport, HttpContext context)
    {
        if (!_userIdentifierProvider.TryGetUserId(context, out Guid userId))
        {
            Logger.Info("Invalid token");
            await _sendOutput.SendAsync(
                transport,
                new OutputEnvelope<InfoSnapshot>(OutputDomain.System, OutputType.Info, new InfoSnapshot(false, "HANDSHAKE_INVALID_TOKEN", "The given token is expired or invalid"))
            );
            return HandShakeResult.Failed("Invalid token");
        }

        // Wait user sends token
        var input = await ReceiveTokenAsync(socket);
        if (input.Status == StatusHandShakeResult.failed || input.Content == null)
        {
            Logger.Info(input.Message);
            await _sendOutput.SendAsync(
                transport,
                new OutputEnvelope<InfoSnapshot>(OutputDomain.System, OutputType.Info, new InfoSnapshot(false, "HANDSHAKE_INVALID_DATA", input.Message))
            );
            return input;
        }

        var content = (HandshakePayLoad)input.Content;

        if (!_roomBindingAccess.IsMember(userId, content.RoomId))
        {
            Logger.Info($"User {userId} is not a member of room {content.RoomId}");
            await _sendOutput.SendAsync(
                transport,
                new OutputEnvelope<InfoSnapshot>(OutputDomain.System, OutputType.Info, new InfoSnapshot(false, "HANDSHAKE_NOT_IN_ROOM", "User is not a member of the room"))
            );
            return HandShakeResult.Failed($"User {userId} is not a member of room {content.RoomId}");
        }

        await _sendOutput.SendAsync(
                transport,
                new OutputEnvelope<InfoSnapshot>(OutputDomain.System, OutputType.Info, new InfoSnapshot(true, "HANDSHAKE_SUCCESS", "Handshake done"))
            );
        return HandShakeResult.Success(content);
    }

    private async Task<HandShakeResult> ReceiveTokenAsync(WebSocket socket)
    {
        try
        {
            var buffer = new byte[1024];

            var result = await socket.ReceiveAsync(buffer, CancellationToken.None);
            var json = Encoding.UTF8.GetString(buffer, 0, result.Count);

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var envelope = JsonSerializer.Deserialize<HandShakeEnvelope<HandshakePayLoad>>(json, options);

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
