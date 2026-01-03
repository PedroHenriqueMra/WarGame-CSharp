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

            var input = (IInput)JsonSerializer.Deserialize(
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

    private void Dispatch(InputDescriptor descriptor, IInput input, Session session)
    {
        switch (descriptor.Group)
        {
            case InputGroup.Gameplay:
                _gameStorage.TryGetRoom(session.User.CurrentRoomId ?? 0, out var room);
                room?.EnqueueCommand((IGameplayCommand)input.ToCommand(session)!);
                break;
            case InputGroup.Admin:
                _gameAdminCommandHandler.Handle((IGameAdminCommand)input.ToCommand(session)!, session);
                break;
            case InputGroup.System:
                _systemAdminCommandHandle.Handle((ISystemAdminCommand)input.ToCommand(session)!, session);
                break;
        }

        // DEBUG: LIST USERS INTO THE ROOM
        if (_gameStorage.TryGetRoom(session.User.CurrentRoomId ?? 1, out var roomm))
        {
            
            foreach(var room in _gameStorage.GetRooms())
            {
                Console.WriteLine($"Room Name: {roomm.RoomName}. Users:");
                foreach(var user in room.Users)
                {
                    Console.WriteLine($"User id {user.UserId}, User name: {user.Username}, current RoomId: {user.CurrentRoomId}");
                }
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
