public sealed class SystemAdminCommandHandle
{

    private readonly GameDataStorage _storage;

    public SystemAdminCommandHandle(GameDataStorage storage)
    {
        _storage = storage;
    }

    public SystemAdminResult? Handle(ISystemAdminCommand command)
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

        return null;
    }

    private SystemAdminResult HandleCreateRoom(CreateRoomCommand cmd)
    {
        User user = _storage.GetUser(cmd.UserId);
        if (user is null)
            return SystemAdminResult.Fail("User not found", "CREATE_ROOM_FAIL");

        if (user.CurrentRoomId != null)
            return SystemAdminResult.Fail("User is already in a room", "CREATE_ROOM_FAIL");

        var result = RoomAdmin.CanCreateRoom(new CreateRoomDto(cmd.UserId, cmd.RoomName));
        if (!result.Status)
            return SystemAdminResult.Fail(result.Message, "CREATE_ROOM_FAIL");

        var roomId = IdGenerator.GenRoomId();
        var room = new Room(
            roomId,
            cmd.UserId,
            cmd.RoomName
        );

        _storage.TrySaveRoom(room);

        var joinRoomCommand = new JoinRoomCommand(roomId, cmd.UserId);
        HandleJoinRoom(joinRoomCommand);

        return SystemAdminResult.Ok("CREATE_ROOM_SUCCESS");
    }

    private SystemAdminResult HandleJoinRoom(JoinRoomCommand cmd)
    {
        Room room = _storage.GetRoom(cmd.RoomId);
        if (room is null)
            return SystemAdminResult.Fail("Room not found", "JOIN_ROOM_FAIL");

        User user = _storage.GetUser(cmd.UserId);
        if (user is null)
            return SystemAdminResult.Fail("User not found", "JOIN_ROOM_FAIL");

        if (user.CurrentRoomId != null)
            return SystemAdminResult.Fail("User is already in a room", "JOIN_ROOM_FAIL");

        var result = room.CanJoin(user);
        if (!result.Status)
            return SystemAdminResult.Fail(result.Message, "JOIN_ROOM_FAIL");

        room.JoinUser(user);
        user.CurrentRoomId = cmd.RoomId;
        return SystemAdminResult.Ok("JOIN_ROOM_SUCCESS");
    }

    private SystemAdminResult HandleLeaveRoom(LeaveRoomCommand cmd)
    {
        Room room = _storage.GetRoom(cmd.RoomId);
        if (room is null)
            return SystemAdminResult.Fail("Room is not in this room", "LEAVE_ROOM_FAIL");

        User user = _storage.GetUser(cmd.UserId);;
        if (user is null)
            return SystemAdminResult.Fail("User not found", "LEAVE_ROOM_FAIL");

        if (room.RoomId != user.CurrentRoomId)
            return SystemAdminResult.Fail("User is not in this room", "LEAVE_ROOM_FAIL");

        room.LeaveUser(user);
        user.CurrentRoomId = null;
        return SystemAdminResult.Ok("LEAVE_ROOM_SUCCESS");
    }
}
