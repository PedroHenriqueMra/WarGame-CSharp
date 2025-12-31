using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

public sealed class InputAdmin
{
    private static NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

    private readonly AdminCommandHandler _adminCommandHandler;
    private readonly GameDataStorage _gameStorage;
    
    public InputAdmin(AdminCommandHandler adminCommandHandler, GameDataStorage gameStorage)
    {
        _adminCommandHandler = adminCommandHandler;
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

            var input = (IInput)JsonSerializer.Deserialize(
                envelope.Payload.GetRawText(),
                descriptor.InputType,
                options
            )!;

            Dispatch(descriptor, input, session);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error message: {ex}");
            return;
        }
    }

    private void Dispatch(InputDescriptor descriptor, IInput input, Session session)
    {
        switch (descriptor.Group)
        {
            case InputGroup.Gameplay:
                _gameStorage.TryGetRoom(session.User.CurrentRoomId ?? 0, out var room);
                room?.EnqueueCommand((IGameplayCommand)input.ToCommand(session)!);
                break;
            case InputGroup.Admin:
                _adminCommandHandler.Handle((IAdminCommand)input.ToCommand(session)!, session);
                break;
            case InputGroup.System:
                break;
        }

        // DEBUG: LIST USERS INTO THE ROOM
        if (_gameStorage.TryGetRoom(session.User.CurrentRoomId ?? 1, out var roomm))
        {
            Logger.Trace($"Room Name: {roomm.RoomName}. Users:");
            
            foreach(var user in roomm.Users)
            {
                Console.WriteLine($"User id {user.UserId}, User name: {user.Username}, current RoomId: {user.CurrentRoomId}");
            }
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
