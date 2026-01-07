public sealed class UserAdminHandler
{
    private readonly GameDataStorage _storage;

    public UserAdminHandler(GameDataStorage storage)
    {
        _storage = storage;
    }

    public User? CreateUser(CreateUserDto dto)
    {
        if (!UserAdmin.CanCreateUser(dto))
            return null;

        User newUser = new User(Guid.NewGuid(), dto.Name, isGuest: false);
        if (!_storage.TrySaveUser(newUser))
            return null;

        return newUser;
    }

    public User? CreateGuest()
    {
        User newGuest = User.CreateGuest();
        if (!_storage.TrySaveUser(newGuest))
            return null;

        return newGuest;
    }
}
