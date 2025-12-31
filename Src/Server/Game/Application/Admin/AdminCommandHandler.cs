public sealed class AdminCommandHandler
{
    private readonly GameDataStorage _storage;

    public AdminCommandHandler(GameDataStorage storage)
    {
        _storage = storage;
    }

    public void Handle(IAdminCommand command, Session session)
    {
        switch (command)
        {
            case CreateRoomCommand create:
                HandleCreateRoom(create, session);
                break;
            case JoinRoomCommand join:
                HandleJoinRoom(join, session);
                break;
            case LeaveRoomCommand leave:
                HandleLeaveRoom(leave, session);
                break;
            case StartGameCommand start:
                HandleStartGame(start, session);
                break;
            case StopGameCommand stop:
                HandleStopGame(stop, session);
                break;
        }
    }
    private void HandleCreateRoom(CreateRoomCommand cmd, Session session)
    {
        var roomId = IdGenerator.GenRoomId();
        var room = new Room(
            roomId,
            session.User.UserId,
            cmd.RoomName
        );

        _storage.AddRoom(room);
    }

    private void HandleJoinRoom(JoinRoomCommand cmd, Session session)
    {
        // Room Rules:
        if (!_storage.TryGetRoom(cmd.RoomId, out Room room))
            return;

        if (!room.CanJoin())
            return;

        room.JoinUser(session.User);
        session.User.CurrentRoomId = cmd.RoomId;
    }
    
    private void HandleLeaveRoom(LeaveRoomCommand cmd, Session session)
    {
        if (!_storage.TryGetRoom(cmd.RoomId, out Room room))
            return;

        if (room.RoomId != session.User.CurrentRoomId)
            return;

        room.LeaveUser(session.User);
        session.User.CurrentRoomId = null;
    }

    private void HandleStartGame(StartGameCommand cmd, Session session)
    {
        if (!_storage.TryGetRoom(cmd.RoomId, out Room room))
            return;

        if (room.RoomId != session.User.CurrentRoomId)
            return;

        room.Start(session.User);
    }

    private void HandleStopGame(StopGameCommand cmd, Session session)
    {
        if (!_storage.TryGetRoom(cmd.RoomId, out Room room))
            return;

        if (room.RoomId != session.User.CurrentRoomId)
            return;

        room.Stop(session.User);
    }
}

