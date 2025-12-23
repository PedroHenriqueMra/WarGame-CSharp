public class RoomAdmin
{
    public Room? TryCreateRoom(CreateRoomDTO data)
    {
        if (data.RoomName == null || data.RoomName == "") return null;
        if (data.AdminId == null) return null;
        if (data.RoomName.Length > 20) return null;
        if (data.RoomName.Length < 3) return null;
        
        int id = 1; // editar depois
        return new Room(id, data.AdminId, data.RoomName);
    }
}
