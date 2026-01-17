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

        //var result = room.CanStart(session.User);
        //if (!result.Status)
        //    return CommandResult.Fail(result.Message, "GAME_CANNOT_START");

        room.Start();
        var playerId = new { PlayerId = room.PlayerByUserInGame.First(p => p.Key == session.User.UserId).Value.Id };
        return CommandResult.Ok("GAME_START_SUCCESS", content: playerId);
    }

    private CommandResult HandleStopGame(StopGameCommand cmd, Session session)
    {
        Room room = _storage.GetRoom(cmd.RoomId);
        if (room is null)
            return CommandResult.Fail("Room not found", "GAME_CANNOT_STOP");

        var result = room.CanStop(session.User);
        if (!result.Status)
            return CommandResult.Fail(result.Message, "GAME_CANNOT_STOP");

        room.Stop();
        return CommandResult.Ok("GAME_STOP_SUCCESS");
    }
}

