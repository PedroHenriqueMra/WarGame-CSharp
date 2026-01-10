public sealed class SystemAdminCommandHandle
{

    private readonly GameDataStorage _storage;

    public SystemAdminCommandHandle(GameDataStorage storage)
    {
        _storage = storage;
    }

    public bool Handle(ISystemAdminCommand command)
    {
        switch (command)
        {
            case CreateRoomCommand create:
                return HandleCreateRoom(create);
            case JoinRoomCommand join:
                return HandleJoinRoom(join);
            case LeaveRoomCommand leave:
                return HandleLeaveRoom(leave);
        }

        return false;
    }

    private bool HandleCreateRoom(CreateRoomCommand cmd)
    {
        User user = _storage.GetUser(cmd.UserId);
        if (user is null)
        {
            return false;
        }

        if (user.CurrentRoomId != null)
            return false;

        if (_storage.GetUser(cmd.UserId) is null)
        {
            return false;
        }

        if (!RoomAdmin.CanCreateRoom(new CreateRoomDto(cmd.UserId, cmd.RoomName)))
            return false;

        var roomId = IdGenerator.GenRoomId();
        var room = new Room(
            roomId,
            cmd.UserId,
            cmd.RoomName
        );

        _storage.TrySaveRoom(room);

        var joinRoomCommand = new JoinRoomCommand(roomId, cmd.UserId);
        HandleJoinRoom(joinRoomCommand);

        return true;
    }

    private bool HandleJoinRoom(JoinRoomCommand cmd)
    {
        Room room = _storage.GetRoom(cmd.RoomId);
        if (room is null)
            return false;

        User user = _storage.GetUser(cmd.UserId);
        if (user is null)
            return false;

        if (user.CurrentRoomId != null)
            return false;

        var result = room.CanJoin(user);
        if (!result.Status)
            return false;

        room.JoinUser(user);
        user.CurrentRoomId = cmd.RoomId;
        return true;
    }

    private bool HandleLeaveRoom(LeaveRoomCommand cmd)
    {
        Room room = _storage.GetRoom(cmd.RoomId);
        if (room is null)
            return false;

        User user = _storage.GetUser(cmd.UserId);;
        if (user is null)
        {
            return false;
        }

        if (room.RoomId != user.CurrentRoomId)
            return false;

        room.LeaveUser(user);
        user.CurrentRoomId = null;
        return true;
    }
}
