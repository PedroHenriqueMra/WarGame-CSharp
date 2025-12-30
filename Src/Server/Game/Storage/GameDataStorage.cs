using System.Collections.Concurrent;

public class GameDataStorage
{
    public readonly ConcurrentDictionary<int, Room> _rooms = new();
    public readonly ConcurrentDictionary<int, User> _users = new();

    public bool TryGetRoom(int roomId, out Room room)
        => _rooms.TryGetValue(roomId, out room);
    public List<Room> GetRooms()
        => _rooms.Values.ToList();

    public void AddRoom(Room room)
        => _rooms.TryAdd(room.RoomId, room);
} 
