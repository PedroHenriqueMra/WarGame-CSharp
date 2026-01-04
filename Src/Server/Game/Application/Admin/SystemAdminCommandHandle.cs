public class SystemAdminCommandHandle
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
        if (!_storage.TryGetUser(cmd.UserId, out User user))
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

        _storage.AddRoom(room);
        return true;
    }

    private bool HandleJoinRoom(JoinRoomCommand cmd)
    {
        // Room Rules:
        if (!_storage.TryGetRoom(cmd.RoomId, out Room room))
            return false;

        User user;
        if (!_storage.TryGetUser(cmd.UserId, out user))
        {
            if (!_storage.TryGetUserGuest(cmd.UserId, out user))
            {
                return false;
            }
        }

        if (!room.CanJoin())
            return false;

        room.JoinUser(user);
        user.CurrentRoomId = cmd.RoomId;
        return true;
    }

    private bool HandleLeaveRoom(LeaveRoomCommand cmd)
    {
        if (!_storage.TryGetRoom(cmd.RoomId, out Room room))
            return false;

        User user;
        if (!_storage.TryGetUser(cmd.UserId, out user))
        {
            if (!_storage.TryGetUserGuest(cmd.UserId, out user))
            {
                return false;
            }
        }

        if (room.RoomId != user.CurrentRoomId)
            return false;

        room.LeaveUser(user);
        user.CurrentRoomId = null;
        return true;
    }
}
