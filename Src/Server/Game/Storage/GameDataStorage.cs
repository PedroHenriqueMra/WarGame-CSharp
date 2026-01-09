public class GameDataStorage
{
    private readonly IUserStore _userStore;
    private readonly IRoomStore _roomStore;
    public GameDataStorage(IUserStore userStore, IRoomStore roomStore)
    {
        _userStore = userStore;
        _roomStore = roomStore;
    }

    // USERS
    public bool TrySaveUser(User user)
        => _userStore.TrySaveUser(user);

    public bool TryDeleteUser(Guid userId)
        => _userStore.TryDeleteUser(userId);

    public User? GetUser(Guid userId)
        => _userStore.GetUserById(userId);

    // ROOMS
    public bool TrySaveRoom(Room room)
        => _roomStore.TrySaveRoom(room);

    public Room? GetRoom(int roomId)
        => _roomStore.GetRoomById(roomId);

    public IReadOnlyList<RoomInfoDto> GetRoomInfos()
        => _roomStore.GetRoomInfos();
}
