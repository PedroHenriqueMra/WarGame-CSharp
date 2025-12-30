public static class RoomAdmin
{
    public static bool CanCreateRoom(CreateRoomDto data)
    {
        if (data.RoomName == null || data.RoomName == "") return false;
        if (data.RoomName.Length > 20) return false;
        if (data.RoomName.Length < 3) return false;
        
        return true;
    }
}
