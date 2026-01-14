using System.Net.WebSockets;

public class User
{
    public Guid UserId { get; private set; }
    public string Username { get; set; }
    public bool IsGuest { get; private set; }
    
    public Guid? CurrentRoomId { get; set; }

    public User (Guid userId, string username, bool isGuest)
    {
        UserId = userId;
        Username = username;
        IsGuest = isGuest;
    }

    public static User CreateGuest()
        => new User(Guid.NewGuid(), "Guest" + new Random().Next(1000, 9999), true);

    public static User CreateAuthenticated(Guid id, string userName)
        => new User(id, userName, false);
}
