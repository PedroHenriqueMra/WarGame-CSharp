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
}
