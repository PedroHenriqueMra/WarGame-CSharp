public sealed class GameAdminCommandHandler
{
    private readonly GameDataStorage _storage;

    public GameAdminCommandHandler(GameDataStorage storage)
    {
        _storage = storage;
    }

    public void Handle(IGameAdminCommand command, Session session)
    {
        switch (command)
        {
            case StartGameCommand start:
                HandleStartGame(start, session);
                break;
            case StopGameCommand stop:
                HandleStopGame(stop, session);
                break;
        }
    }

    private void HandleStartGame(StartGameCommand cmd, Session session)
    {
        Room room = _storage._roomStore.GetRoomById(cmd.RoomId);
        if (room is null)
            return;

        if (room.RoomId != session.User.CurrentRoomId)
            return;

        room.Start(session.User);
    }

    private void HandleStopGame(StopGameCommand cmd, Session session)
    {
        Room room = _storage._roomStore.GetRoomById(cmd.RoomId);
        if (room is null)
            return;

        if (room.RoomId != session.User.CurrentRoomId)
            return;

        room.Stop(session.User);
    }
}

