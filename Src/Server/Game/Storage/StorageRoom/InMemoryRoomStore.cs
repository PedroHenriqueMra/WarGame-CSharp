using System.Collections.Concurrent;

public sealed class InMemoryRoomStore : IRoomStore
{
    private readonly ConcurrentDictionary<int, Room> _rooms = new();
    
    public bool TrySaveRoom(Room room)
    {
        return _rooms.TryAdd(room.RoomId, room);
    }
    public bool TryDeleteRoom(int roomId)
    {
        return _rooms.TryRemove(roomId, out _);
    }
    public Room? GetRoomById(int roomId)
    {
        return _rooms.GetValueOrDefault(roomId);
    }

    public IEnumerable<Room> GetRooms()
    {
        return _rooms.Values;
    }
    public IReadOnlyList<RoomInfoDto> GetRoomInfos()
    {
        return _rooms.Values.Select(r => new RoomInfoDto(r.RoomId, r.RoomName, r.Users.Count, r.IsRunning)).ToList();
    }
}
