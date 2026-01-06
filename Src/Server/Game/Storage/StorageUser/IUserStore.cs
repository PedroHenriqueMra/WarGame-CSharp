public interface IUserStore
{
    public bool TrySaveGuest(User guest);
    public bool TrySaveUser(User user);
    public bool TryDeleteUser(Guid userId);
    public bool TryDeleteGuest(Guid guestId);
    public User? GetUserById(Guid userId);
    public User? GetGuestById(Guid guestId);
}
