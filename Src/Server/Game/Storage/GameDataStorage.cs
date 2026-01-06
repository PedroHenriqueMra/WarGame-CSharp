using System.Collections.Concurrent;

public class GameDataStorage
{
    public readonly InMemoryUserStore _userStore = new();
    public readonly InMemoryRoomStore _roomStore = new();
} 
