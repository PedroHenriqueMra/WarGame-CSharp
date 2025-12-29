using Microsoft.AspNetCore.Http.HttpResults;

public class Bot
{
    private static NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

    public Room Room { get; set; }
    public Player Player { get; set; }

    private RoomAdmin _roomAdmin;
    private PlayerAdmin _playerAdmin;

    public Bot()
    {
        this._roomAdmin = new RoomAdmin();
        this._playerAdmin = new PlayerAdmin();
    }

    public void CreateRoom()
    {
        this._roomAdmin.TryCreateRoom(new CreateRoomDTO(1, "BOT Room"), out Room room);
        this.Room = room;
        //Logger.Trace("Room reated!");
    }

    public void startGame()
    {
        if (this.Room != null)
        {
            //Logger.Trace("Initializanding room...");
            this.Room.Start();
        }
    }

    public void CreatePlayer()
    {
        this._playerAdmin.TryCreatePlayer(new CreatePlayerDTO("BOT"), out Player player);
        this.Player = player;
        //Logger.Trace("Player created!");
    }

    public void JoinRoom()
    {
        this._roomAdmin.JoinPlayer(new JoinPlayerDTO(this.Room, this.Player));
           
        //Logger.Trace("Player Joined room!");
    }

    public async Task PlayBotCommandsAsync()
    {
        IGameCommand[] commandsList = new IGameCommand[]
        {
            new MovePlayerCommand(1, 1), // go ahead
            new MovePlayerCommand(1, -1), // go back
        };

        var command=commandsList[0];
        for (int i = 0;i < 15;i++)
        {
            command = commandsList[1];
            if (i < 10)
                command = commandsList[0];

            this.Room.EnqueueCommand(command);
            //Logger.Trace("Bot send a Command!");
            await Task.Delay(300);
        }
    }
}
