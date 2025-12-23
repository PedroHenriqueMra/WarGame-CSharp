public class Room
{
    public int RoomId { get; private set; }
    public int AdminId { get; private set; }
    public string RoomName { get; set; }

    private GameLoop _gameLoop;
    private Game _game;
    public Room (int id, int adminId, string name)
    {
        this.RoomId = id;
        this.AdminId = adminId;
        this.RoomName = name;
    }

    public void Start()
    {
        this._game = new Game();
        this._gameLoop = new GameLoop(_game);
        _gameLoop.Start();
    }

    public async Task StopAsync()
    {
        await _gameLoop.StopAsync();
    }
}
