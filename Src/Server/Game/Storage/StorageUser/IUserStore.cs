public interface IUserStore
{
    public bool TrySaveUser(User user);
    public bool TryDeleteUser(Guid userId);
    public User? GetUserById(Guid userId);
}
