public class Room
{
    public int RoomId { get; private set; }
    public int AdminId { get; private set; }
    public string RoomName { get; set; }
    //public List<User> Users { get; set; } = new List<User>();

    private RoomRestrictions _restrictions = new();
    private Game _game;
    private GameLoop _gameLoop;
    public Room (int id, int adminId, string name)
    {
        this.RoomId = id;
        this.AdminId = adminId;
        this.RoomName = name;
    }
    
    public bool CanJoin()
    {
        return _restrictions.CanJoin(_game.Players.Count) && _game.GameState == GameState.Waiting;
    }
    public void JoinPlayer(Player player)
    {
        if (!CanJoin()) return;
        this._game.AddPlayer(player);
    }

    public void EnqueueCommand(IGameCommand command)
    {
        this._game.EnqueueCommand(command);
    }

    public void Start()
    {
        this._game = new Game();
        this._gameLoop = new GameLoop(_game);
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
        await _gameLoop.StopAsync();
    }
}
