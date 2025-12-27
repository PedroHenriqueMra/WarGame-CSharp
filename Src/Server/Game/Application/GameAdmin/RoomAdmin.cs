public class RoomAdmin
{
    public bool TryCreateRoom(CreateRoomDTO data, out Room? room)
    {
        room = null;

        if (data.RoomName == null || data.RoomName == "") return false;
        if (data.RoomName.Length > 20) return false;
        if (data.RoomName.Length < 3) return false;
        
        int id = 1; // editar depois
        room = new Room(id, data.AdminId, data.RoomName);
        return true;
    }

    public bool JoinPlayer(JoinPlayerDTO data)
    {
        if (data.Room == null) return false;
        if (data.player == null) return false;

        data.Room.JoinPlayer(data.player);
        return true;
    }
}
