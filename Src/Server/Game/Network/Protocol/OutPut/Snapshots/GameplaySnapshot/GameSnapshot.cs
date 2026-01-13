public class GameSnapshot : IPayload
{
    public long Tick { get; set; }
    public bool IsRunning { get; set; }
    public IReadOnlyList<PlayerSnapshot> Players { get; set; }
    public GameSnapshot(long tick, bool isRunning, IReadOnlyList<PlayerSnapshot> players)
    {
        Tick = tick;
        IsRunning = isRunning;
        Players = players;
    }
}
