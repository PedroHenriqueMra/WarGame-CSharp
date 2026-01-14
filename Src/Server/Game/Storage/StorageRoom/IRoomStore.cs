public interface IRoomStore
{
    public bool TrySaveRoom(Room room);
    public bool TryDeleteRoom(Guid roomId);
    public Room? GetRoomById(Guid roomId);

    public IEnumerable<Room> GetRooms();
    public IReadOnlyList<RoomInfoDto> GetRoomInfos();
}
