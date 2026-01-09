public class GameSnapshot : IPayload
{
    public long Tick { get; set; }
    public IReadOnlyList<PlayerSnapshot> Players { get; set; }
    public GameSnapshot(long tick, IReadOnlyList<PlayerSnapshot> players)
    {
        Tick = tick;
        Players = players;
    }
}
