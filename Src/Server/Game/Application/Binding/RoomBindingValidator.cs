public sealed class RoomBindingValidator : IRoomBindingValidator
{
    private readonly GameDataStorage _gameDataStorage;
    public RoomBindingValidator(GameDataStorage gameDataStorage)
    {
        _gameDataStorage = gameDataStorage;
    }

    public bool IsMember(Guid userId, int roomId)
    {
        Room? room = _gameDataStorage.GetRoom(roomId);
        if (room == null)
        {
            return false;
        }

        return room.Users.Any(user => user.UserId == userId);
    }
}
