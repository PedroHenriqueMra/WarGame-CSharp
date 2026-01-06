using System.Collections.Concurrent;

public sealed class InMemoryUserStore : IUserStore
{
    private readonly ConcurrentDictionary<Guid, User> _users = new();
    private readonly ConcurrentDictionary<Guid, User> _guests = new();

    public bool TrySaveGuest(User guest)
    {
        if (!guest.IsGuest)
            return false;

        _guests[guest.UserId] = guest;
        return true;
    }
    public bool TrySaveUser(User user)
    {
        if (user.IsGuest)
            return false;

        _users[user.UserId] = user;
        return true;
    }
    public bool TryDeleteUser(Guid userId)
    {
        if (_users.TryRemove(userId, out _))
            return true;

        return false;
    }
    public bool TryDeleteGuest(Guid guestId)
    {
        if (_guests.TryRemove(guestId, out _))
            return true;

        return false;
    }

    public User? GetUserById(Guid userId)
    {
        if (_users.TryGetValue(userId, out var user))
            return user;

        return null;
    }
    public User? GetGuestById(Guid guestId)
    {
        if (_guests.TryGetValue(guestId, out var guest))
            return guest;

        return null;
    }
}
