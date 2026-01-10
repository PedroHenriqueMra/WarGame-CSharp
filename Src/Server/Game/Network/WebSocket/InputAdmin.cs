using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Threading.Tasks;

public sealed class InputAdmin
{
    private readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

    private readonly GameAdminCommandHandler _gameAdminCommandHandler;
    private readonly GameDataStorage _gameStorage;

    private readonly SendOutput _sendOutput; 
    
    public InputAdmin(GameAdminCommandHandler gameAdinCommandHandler, GameDataStorage gameStorage, SendOutput sendOutput)
    {
        _gameAdminCommandHandler = gameAdinCommandHandler;
        _gameStorage = gameStorage;
        _sendOutput = sendOutput;
    }

    public void Handle(string json, Session session)
    {
        try
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var envelope = JsonSerializer.Deserialize<InputEnvelope>(json, options);
            if (envelope == null)
                return;

            var descriptor = InputRegistry.GetDescriptor(envelope.Type);
            if (descriptor == null)
                return;

            if (!descriptor.AllowPayload && HasAnyProperty(envelope.Payload))
                return;

            var input = JsonSerializer.Deserialize(
                envelope.Payload.GetRawText(),
                descriptor.InputType,
                options
            )!;

            Dispatch(descriptor, input, session);
        }
        catch (Exception ex)
        {
            Logger.Error($"Error message: {ex}");
            return;
        }
    }

    private async Task Dispatch(InputDescriptor descriptor, object input, Session session)
    {
        switch (descriptor.Group)
        {
            case InputGroup.Gameplay:
                var gameplayInput = (IGameplayInput)input;
                var gameplayCommand = gameplayInput.ToCommand(session);

                Room room = _gameStorage.GetRoom(session.User.CurrentRoomId.Value);
                room?.EnqueueCommand(gameplayCommand!);
                break;
            case InputGroup.Admin:
                var gameAdminInput = (IGameAdminInput)input;
                var gameAdminCommand = gameAdminInput.ToCommand(session);

                var result = _gameAdminCommandHandler.Handle(gameAdminCommand!, session);
                if (result is not null && !result.Success)
                {
                    await _sendOutput.SendAsync(
                        new WebSocketTransport(session.Socket),
                        new OutputEnvelope<InfoSnapshot>(OutputDomain.Game, OutputType.Info, new InfoSnapshot(true, result.Code, result.ErrorMessage!))
                    );
                }
                break;
        }
    }

    private static bool HasAnyProperty(JsonElement element)
    {
        if (element.ValueKind != JsonValueKind.Object)
            return false;

        using var e = element.EnumerateObject();
        return e.MoveNext();
    }
}
