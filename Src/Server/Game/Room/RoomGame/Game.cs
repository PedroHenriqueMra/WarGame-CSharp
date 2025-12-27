using System.Collections.Concurrent;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using System.Numerics;

public class Game
{
    private static NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
    private readonly ConcurrentQueue<IGameCommand> _commandQueue = new();
    public List<Player> Players { get; private set; } = new();
    public GameState GameState { get; private set; } = GameState.Waiting;
    public IMapGame Map { get; private set; } = new MapGrid(); // <- MapGrid test

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

    //public void Start()
    //{
    //    this.GameState = GameState.Running;
    //}

    public void Update(float deltaTime)
    {
        // Record Intention
        while (this._commandQueue.TryDequeue(out var command))
        {
            command.Execute(this, deltaTime);
            //Logger.Trace($"Command {command.ToString()} executed");
        }

        // Executing gameplay:
        foreach (var player in this.Players)
        {
            // calculate next horizontal move
            player.CurrentVelocity = new Vector2(player.Direction.DirectionX * player.Speed * deltaTime, 0f);
            float dx = player.Position.X + player.CurrentVelocity.X;

            if (Map.IsInsideOfMap(dx, 0f))
            {
                player.Position.X = dx;
            }
            else
            {
                player.Position.X = dx > Map.Width ? Map.Width : 0;
            }
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
