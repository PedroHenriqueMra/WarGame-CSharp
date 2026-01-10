public sealed class GameAdminCommandHandler
{
    private readonly GameDataStorage _storage;

    public GameAdminCommandHandler(GameDataStorage storage)
    {
        _storage = storage;
    }

    public CommandResult? Handle(IGameAdminCommand command, Session session)
    {
        switch (command)
        {
            case StartGameCommand start:
                return HandleStartGame(start, session);
            case StopGameCommand stop:
                return HandleStopGame(stop, session);
        }

        return null;
    }

    private CommandResult HandleStartGame(StartGameCommand cmd, Session session)
    {
        Room room = _storage.GetRoom(cmd.RoomId);
        if (room is null)
            return CommandResult.Fail("Room not found", "GAME_CANNOT_START");

        if (room.RoomId != session.User.CurrentRoomId)
            return CommandResult.Fail("User is not in the room", "GAME_CANNOT_START");

        if (!room.CanStart(session.User))
            return CommandResult.Fail("Room cannot start", "GAME_CANNOT_START");

        room.Start(session.User);
        return CommandResult.Ok("GAME_START_SUCCESS");
    }

    private CommandResult HandleStopGame(StopGameCommand cmd, Session session)
    {
        Room room = _storage.GetRoom(cmd.RoomId);
        if (room is null)
            return CommandResult.Fail("Room not found", "GAME_CANNOT_STOP");

        if (room.RoomId != session.User.CurrentRoomId)
            return CommandResult.Fail("User is not in the room", "GAME_CANNOT_STOP");

        room.Stop(session.User);
        return CommandResult.Ok("GAME_STOP_SUCCESS");
    }
}

