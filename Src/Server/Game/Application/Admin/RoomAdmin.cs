public static class RoomAdmin
{
    public static SystemAdminResult CanCreateRoom(CreateRoomDto data)
    {
        if (data.RoomName == null || data.RoomName == "")
            return SystemAdminResult.Fail("Room name is empty", "CAN_CREATE_ROOM_FAIL");
        if (data.RoomName.Length > 20)
            return SystemAdminResult.Fail("Room name too long!. It must be at most 20 characters", "CAN_CREATE_ROOM_FAIL");
        if (data.RoomName.Length < 3)
            return SystemAdminResult.Fail("Room name too short!. It must be at least 3 characters", "CAN_CREATE_ROOM_FAIL");
        
        return SystemAdminResult.Ok("CAN_CREATE_ROOM_SUCCESS");
    }
}
