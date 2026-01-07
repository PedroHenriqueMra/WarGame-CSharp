using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

public sealed class InputAdmin
{
    private readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

    private readonly GameAdminCommandHandler _gameAdminCommandHandler;
    private readonly SystemAdminCommandHandle _systemAdminCommandHandle;
    private readonly GameDataStorage _gameStorage;
    
    public InputAdmin(GameAdminCommandHandler gameAdinCommandHandler, SystemAdminCommandHandle systemAdminCommandHandle, GameDataStorage gameStorage)
    {
        _gameAdminCommandHandler = gameAdinCommandHandler;
        _systemAdminCommandHandle = systemAdminCommandHandle;
        _gameStorage = gameStorage;
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

    private void Dispatch(InputDescriptor descriptor, object input, Session session)
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

                _gameAdminCommandHandler.Handle(gameAdminCommand!, session);
                break;
            case InputGroup.System:
                var systemAdminInput = (ISystemAdminInput)input;
                var systemAdminCommand = systemAdminInput.ToCommand(session.User.UserId);

                _systemAdminCommandHandle.Handle(systemAdminCommand!);
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
