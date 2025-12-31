using System.Collections.Concurrent;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using System.Numerics;

public class Game
{
    private static NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

    private readonly ConcurrentQueue<IGameplayCommand> _commandQueue = new();
    
    public List<Player> Players { get; set; } = new(); // PUBLIC FOR TESTS
    private Dictionary<Guid, Player> _playersByUserId = new();
    private int _playerIdSeq = 0;

    public GameState GameState { get; private set; } = GameState.Waiting;
    
    public IMapGame Map { get; private set; } = new MapGrid(); // <- MapGrid test
    private readonly PlayerPhysics _playerPhysics = new PlayerPhysics();

    public void EnqueueCommand(IGameplayCommand command)
    {
        this._commandQueue.Enqueue(command);
    }

    public void AddPlayer(Player player)
    {
        this.Players.Add(player);

        _playersByUserId[player.UserId] = player;
    }
    public Player? GetPlayerByUserId(Guid userId)
    {
        _playersByUserId.TryGetValue(userId, out var player);
        return player;
    }

    public Player CreatePlayer(Guid userId, string name)
    {
        var id = ++_playerIdSeq;
        return new Player(id, userId, name);
    }

    public void RemovePlayer(Player player)
    {
        _playersByUserId.Remove(player.UserId);
        this.Players.Remove(player);
    }

    public int CountPlayers() => Players.Count;

    public void Start()
    {
        this.GameState = GameState.InProgress;
    }

    public void Stop()
    {
        this.GameState = GameState.Finished;

        Players.Clear();
        _playersByUserId.Clear();
        _commandQueue.Clear();
    }

    public void Update(float deltaTime)
    {
        // Record Intention
        while (this._commandQueue.TryDequeue(out var command))
        {
            if (command == null)
                continue;

            command.Execute(this);
            Logger.Trace($"{command} executed");
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
    InProgress,
    Paused,
    Finished
}
