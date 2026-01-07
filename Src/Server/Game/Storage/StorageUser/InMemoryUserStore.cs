using System.Collections.Concurrent;

public sealed class InMemoryUserStore : IUserStore
{
    private readonly ConcurrentDictionary<Guid, User> _users = new();

    public bool TrySaveUser(User user)
    {
        _users[user.UserId] = user;
        return true;
    }
    public bool TryDeleteUser(Guid userId)
    {
        if (_users.TryRemove(userId, out _))
            return true;

        return false;
    }

    public User? GetUserById(Guid userId)
    {
        if (_users.TryGetValue(userId, out var user))
            return user;

        return null;
    }
}
