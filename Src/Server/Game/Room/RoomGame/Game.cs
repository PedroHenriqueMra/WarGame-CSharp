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
    private readonly PlayerPhysics _playerPhysics = new PlayerPhysics();

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
            command.Execute(this);
            //Logger.Trace($"Game consumed: {command.ToString()} command");
        }

        // Executing gameplay:
        foreach (var player in this.Players)
        {
            // calculate horizontal move:
            player.CurrentVelocity = new Vector2(_playerPhysics.MoveHorizontal(player, Map, deltaTime), player.CurrentVelocity.Y);
            float nextX = player.Position.X + player.CurrentVelocity.X;

            nextX = Math.Clamp(nextX, 0, Map.Width);

            // Horizontal colision
            if (Map.IsInsideOfMap(nextX, player.Position.Y))
                player.Position.X = nextX;

            // calculate vertical move:
            player.IsGrounded = Map.IsWalkeble(player.Position.X, player.Position.Y);

            // jump
            if (player.IsGrounded && player.JumpRequest)
            {
                player.CurrentVelocity = new Vector2(
                    player.CurrentVelocity.X,
                    player.JumpForce
                );
                player.IsGrounded = false;
            }

            _playerPhysics.ApplyVerticalPhysics(player, Map, deltaTime);

            // player update() in end. It will reset intentions
            player.Update();
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
