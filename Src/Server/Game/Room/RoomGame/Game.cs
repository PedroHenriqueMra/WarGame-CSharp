using System.Collections.Concurrent;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;


public class Game
{
    private static NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
    //private readonly ConcurrentQueue<IGameCommand> _commandQueue = new(); // <- after
    private readonly Queue<IGameCommand> _commandQueue = new();
    public List<Player> Players { get; private set; } = new();
    //public World World { get; private set; } = new();
    public GameState GameState { get; private set; } = GameState.Waiting;
    
    public void EnqueueCommand(IGameCommand command)
    {
        this._commandQueue.Enqueue(command);
    }

    public void AddPlayer(Player player)
    {
        this.Players.Add(player);
    }
    public void RemovePlayer(Player player)
    {
        this.Players.Remove(player);
    }

    public void Start()
    {
        Logger.Info("Game State: Running");
    }

    public void Update(float deltaTime)
    {
        while (this._commandQueue.TryDequeue(out var command))
        {
            command.Execute(this, deltaTime);
            Logger.Trace($"Command {command.ToString()} executed");
        }
    }
}

public enum GameState
{
    Waiting,
    Running,
    Paused,
    Finished
}
