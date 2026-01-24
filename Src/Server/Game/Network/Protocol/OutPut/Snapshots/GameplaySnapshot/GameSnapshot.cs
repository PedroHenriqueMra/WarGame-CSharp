public class GameSnapshot : IPayload
{
    public long Tick { get; set; }
    public bool IsRunning { get; set; }
    public IReadOnlyList<PlayerSnapshot> Players { get; set; }
    public int LastReceivedInputTick { get; set; }
    public GameSnapshot(long tick, bool isRunning, IReadOnlyList<PlayerSnapshot> players, int lastReceivedInputTick)
    {
        Tick = tick;
        IsRunning = isRunning;
        Players = players;
        LastReceivedInputTick = lastReceivedInputTick;
    }
}
