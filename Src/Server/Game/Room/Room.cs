public class Room
{
    public int RoomId { get; private set; }
    public Guid AdminId { get; private set; }
    public string RoomName { get; set; }
    public List<User> Users { get; set; } = new List<User>();
    public Dictionary<Guid, Player>? PlayerByUserInGame { get; private set; }

    private RoomRestrictions _restrictions = new();
    private Game _game;
    private GameLoop _gameLoop;
    public Room(int id, Guid adminId, string name)
    {
        this.RoomId = id;
        this.AdminId = adminId;
        this.RoomName = name;
    }

    public bool CanJoin()
    {
        if (_game == null)
            return _restrictions.CanJoin(Users.Count);

        return _restrictions.CanJoin(_game.CountPlayers())
               && _game.GameState == GameState.Waiting;
    }
    public void JoinUser(User user)
    {
        Users.Add(user);
    }

    public void LeaveUser(User user)
    {
        Users.Remove(user);
    }

    public void EnqueueCommand(IGameplayCommand command)
    {
        this._game.EnqueueCommand(command);
    }

    public void Start()
    {
        this._game = new Game();
        this._gameLoop = new GameLoop(_game);

        PlayerByUserInGame = new();

        foreach (User user in Users)
        {
            // Game creates player for increment player id. Player ids exist only in game.
            Player newPlayer = _game.CreatePlayer(user.UserId, user.Username);

            _game.AddPlayer(newPlayer);
            PlayerByUserInGame[user.UserId] = newPlayer;
        }

        try
        {
            _gameLoop.Start();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async Task StopAsync()
    {
        _game.Stop();
        await _gameLoop.StopAsync();

        PlayerByUserInGame = null;
    }
}
