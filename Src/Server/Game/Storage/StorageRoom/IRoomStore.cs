public interface IRoomStore
{
    public bool TrySaveRoom(Room room);
    public bool TryDeleteRoom(int roomId);
    public Room? GetRoomById(int roomId);

    public IEnumerable<Room> GetRooms();
    public IReadOnlyList<RoomInfoDto> GetRoomInfos();
}
