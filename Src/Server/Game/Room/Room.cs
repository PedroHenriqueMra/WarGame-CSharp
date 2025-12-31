public class Room
{
    private static NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

    public int RoomId { get; private set; }
    public Guid AdminId { get; private set; }
    public string RoomName { get; set; }

    private readonly SendOutput _sendOutputService = new();

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
        
        if (_game?.GameState == GameState.InProgress)
        {
            _game.RemovePlayer(_game.GetPlayerByUserId(user.UserId));
            if (_game.CountPlayers() <= 1)
                StopAsync().Wait();
        }
    }

    public void EnqueueCommand(IGameplayCommand command)
    {
        this._game.EnqueueCommand(command);
    }

    private bool CanStart(User starter)
    {
        if (starter.UserId != AdminId)
            return false;

        if (_restrictions.MinPlayers < Users.Count)
            return false;

        if (_game?.GameState == GameState.InProgress)
            return false;

        return true;
    }

    private void BroadcastSnapshot(GameSnapshot snapshot)
    {
        foreach (User user in Users)
        {
            Session? session = SessionManager.GetSessionByUserId(user.UserId);
            if (session != null)
                _sendOutputService.sendAsync(session.Value, new OutputEnvelope("GameSnapshot", snapshot));
        }
    }

    public void Start(User starter)
    {
        if (!CanStart(starter))
            return;

        this._game = new Game();
        this._gameLoop = new GameLoop(_game);

        // share LoopGame snapshot event
        this._gameLoop.OnSnapshot += (snapshot) =>
        {
            BroadcastSnapshot(snapshot);
        };

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
            _game.Start();
            _gameLoop.Start();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public void Stop(User stopper)
    {
        if (stopper.UserId != AdminId)
            return;

        StopAsync().Wait();
    }

    private async Task StopAsync()
    {
        _game.Stop();
        await _gameLoop.StopAsync();

        PlayerByUserInGame = null;
    }
}
