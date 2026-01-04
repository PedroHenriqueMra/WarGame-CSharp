using System.Collections.Concurrent;

public class GameDataStorage
{
    public readonly ConcurrentDictionary<int, Room> _rooms = new();
    public readonly ConcurrentDictionary<Guid, User> _users = new();
    public readonly ConcurrentDictionary<Guid, User> _guests = new();

    public bool TryGetRoom(int roomId, out Room room)
        => _rooms.TryGetValue(roomId, out room);

    public IReadOnlyList<RoomListDto> GetRoomInfos()
        => _rooms.Values.Select(r => new RoomListDto(
            r.RoomId, 
            r.RoomName,
            r.Users.Count,
            r.IsRunning
            )).ToList();

    public bool TryGetUser(Guid userId, out User user)
        => _users.TryGetValue(userId, out user);

    public bool TryGetUserGuest(Guid guestId, out User guest)
        => _guests.TryGetValue(guestId, out guest);

    public List<Room> GetRooms()
        => _rooms.Values.ToList();

    public void AddRoom(Room room)
        => _rooms.TryAdd(room.RoomId, room);
} 
