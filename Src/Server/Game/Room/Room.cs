public class Room
{
    public int RoomId { get; private set; }
    public Guid AdminId { get; private set; }
    public string RoomName { get; set; }

    private readonly SendOutput _sendOutputService = new();

    public List<User> Users { get; set; } = new List<User>();
    public Dictionary<Guid, Player>? PlayerByUserInGame { get; private set; }
    public bool IsRunning => _game != null && _game.GameState == GameState.InProgress;

    private RoomRestrictions _restrictions = new();
    private Game _game;
    private GameLoop _gameLoop;
    public Room(int id, Guid adminId, string name)
    {
        this.RoomId = id;
        this.AdminId = adminId;
        this.RoomName = name;
    }

    public RoomJoinResult CanJoin(User user)
    {
        if (user.CurrentRoomId != null)
            return RoomJoinResult.Fail("This room not exists");

        if (user.CurrentRoomId == RoomId)
            return RoomJoinResult.Fail("This user is already in this room");
        if (user.CurrentRoomId != null)
            return RoomJoinResult.Fail("This user already is in some room");

        if (_game != null && _game.GameState == GameState.InProgress)
            return RoomJoinResult.Fail("The game is already running");
            
        if (Users.Count >= _restrictions.MaxPlayers)
            return RoomJoinResult.Fail("The room is full");

        return RoomJoinResult.Ok();
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

    public RoomStartResult CanStart(User starter)
    {
        if (starter.UserId != AdminId)
            return RoomStartResult.Fail("You are not the admin of this room");

        if (starter.CurrentRoomId != RoomId)
            return RoomStartResult.Fail("You are not in this room");

        if (Users.Count < _restrictions.MinPlayers)
            return RoomStartResult.Fail($"Not enough players. Minimum players required: {_restrictions.MinPlayers}");

        if (_game?.GameState == GameState.InProgress)
            return RoomStartResult.Fail("Game is already running");

        return RoomStartResult.Ok();
    }

    public void Start()
    {
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

    private void BroadcastSnapshot(GameSnapshot snapshot)
    {
        foreach (User user in Users)
        {
            Session? session = SessionManager.GetSessionByUserId(user.UserId);
            if (session != null)
                _sendOutputService.SendAsync(new WebSocketTransport(session.Socket), new OutputEnvelope<GameSnapshot>(OutputDomain.Game, OutputType.Snapshot, snapshot));
        }
    }

    private async Task StopAsync()
    {
        _game.Stop();
        await _gameLoop.StopAsync();

        PlayerByUserInGame = null;
    }
}
